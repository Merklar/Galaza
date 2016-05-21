using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public  class GameManager : MonoBehaviour
{

    #region TextField
    public Text scoreText;
    public Text levelText;
    public Text gameOverText;
    public Text nextLevelText;
    public Text bonusLengthText;
    #endregion

    #region Variables 
    public int level;
    public int life;
    public static int score;
    public bool onPlayerLive = true;
    public bool onPause = true;
    private float ENEMY_X_POSITION;
    private float ENEMY_Y_POSITION;
    private float idif = 1f;
    private bool onSpeedBonus = false;
    private TextAsset[] _levels;
    #endregion

    #region GameObject 
    public GameObject LifeIcon;
    public GameObject bonusIcon;
    public GameObject playerPrefabs;
    private GameObject player;
    public GameObject enemys;
    public GameObject plRocket;
    public GameObject enRocket;
    public GameObject enemysPrefabs;
    private Object currentLifeIcon;
    public GameObject slider;
    public GameObject enemy_1_prefab;
    public GameObject enemy_2_prefab;
    public GameObject enemy_3_prefab;
    public GameObject playerBornAnimation;
    #endregion

    #region Constans 
    private const float PLAYER_Y = -4.3f;
    private const float PLAYER_X = 0;
    private const byte PLAYER_LIFE_NUMBER = 3;
    private const byte TIME_REBORN = 3;
    private const byte NUMBER_COLONS_ENEMY = 9;

     [SerializeField]
    private Vector3 LEFT_ENEMY_BORN_POSITION = new Vector3(-9f, -6f, 0);
     [SerializeField]
    private Vector3 RIGHT_ENEMY_BORN_POSITION = new Vector3(8f, -6f, 0);
     [SerializeField]
     private Vector3 ENEMY_CENTER_POSITION = new Vector3(0, 3, 0);

    public static readonly Vector3 PLAYER_LIFE_1_POSITION = new Vector3(-5.7f, -4.7f, 0);
    public static readonly Vector3 PLAYER_LIFE_2_POSITION = new Vector3(-5.2f, -4.7f, 0);
    public static readonly Vector3 PLAYER_LIFE_3_POSITION = new Vector3(-4.7f, -4.7f, 0);
    public static readonly Vector3[] PLAYER_LIFE_POSITIONS = {PLAYER_LIFE_1_POSITION, PLAYER_LIFE_2_POSITION, PLAYER_LIFE_3_POSITION };
    public static List<Object> playerLifeIcon = new List<Object>();
    [HideInInspector]
    public MerkPooling pool;
    #endregion

    #region Singltone 
    //Написание класса Синглтона
    private static GameManager __instance;
    public static GameManager Instance
    {
        get
        {
            if (__instance == null)
            {
                __instance = FindObjectOfType<GameManager>();
                if (__instance == null) Debug.LogError("No GameManager!!!");
                return __instance;
            }
            return __instance;
        }
    }
    #endregion

    void Start()
    {
        onStart();
        DOTween.Init(true, true);
        StartCoroutine(PlayerBorn());
        //createPooling();
    }

    public void setChangeScore(int _score)
    {
        score += _score;
        scoreText.text = "SCORE: " + score;
        int numberOfChildren = enemys.transform.childCount;
        if (numberOfChildren-1 <= 0)
        {
            StartCoroutine(darkSlider());
            StartCoroutine(nextLevel());
        }
    }

    private void onStart()
    {
        slider.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1f);
        _levels = Resources.LoadAll<TextAsset>("Levels");
        StartCoroutine(liteSlider());
        ENEMY_X_POSITION = enemys.transform.position.x;
        ENEMY_Y_POSITION = enemys.transform.position.y;
        level = 1;
        life = 3;
        score = 0;
        scoreText.text = "SCORE: " + score;
        levelText.text = "LEVEL: " + level;
        gameOverText.enabled = false;
        nextLevelText.enabled = false;
        loadEnemy();
        for (byte i = 0; i < life; i++ )
        {
            currentLifeIcon = Instantiate(LifeIcon, PLAYER_LIFE_POSITIONS[i], LifeIcon.transform.rotation);
            playerLifeIcon.Add(currentLifeIcon);
        }
    }
    private void loadEnemy()
    {
        string _levlStr = _levels[level - 1].text;
        byte _numberOfEnemy = 0;
        GameObject __enemy;
        byte ix = 0;
        byte iy = 0;

        foreach (char c in _levlStr)
        {
            Vector3 position = new Vector3(ix * 1f - 4f, -iy * 1f + 4f, 0);
            ix++;
            switch (c)
            {
                case '.':
                    break;
                case '1':
                    __enemy = Instantiate(enemy_1_prefab, LEFT_ENEMY_BORN_POSITION, enemy_1_prefab.transform.rotation) as GameObject;
                    __enemy.transform.parent = enemys.transform;
                    __enemy.GetComponent<EnemyParamiters>().tempPosition = position;
                    __enemy.GetComponent<EnemyParamiters>().centralPosition = ENEMY_CENTER_POSITION;
                    _numberOfEnemy++;
                    break;
                case '2':
                    __enemy = Instantiate(enemy_2_prefab, LEFT_ENEMY_BORN_POSITION, enemy_2_prefab.transform.rotation) as GameObject;
                    __enemy.transform.parent = enemys.transform;
                    __enemy.GetComponent<EnemyParamiters>().tempPosition = position;
                    __enemy.GetComponent<EnemyParamiters>().centralPosition = ENEMY_CENTER_POSITION;
                    _numberOfEnemy++;
                    break;
                case '3':
                    __enemy = Instantiate(enemy_3_prefab, LEFT_ENEMY_BORN_POSITION, enemy_3_prefab.transform.rotation) as GameObject;
                    __enemy.transform.parent = enemys.transform;
                    __enemy.GetComponent<EnemyParamiters>().tempPosition = position;
                    __enemy.GetComponent<EnemyParamiters>().centralPosition = ENEMY_CENTER_POSITION;
                    _numberOfEnemy++;
                    break;
                default:
                    ix--;
                    break;
            }
            if (ix == NUMBER_COLONS_ENEMY)
            {
                ix = 0;
                iy++;
            }
        }
        for (byte i = 0; i < _numberOfEnemy; i++)
        {
            if ( i % 2 == 0) {
            enemys.transform.GetChild(i).transform.position = LEFT_ENEMY_BORN_POSITION;
            } else {
            enemys.transform.GetChild(i).transform.position = RIGHT_ENEMY_BORN_POSITION;
            }
            
        }
        Debug.Log("Количество врагов на уровне: "+ _numberOfEnemy);

        StartCoroutine(movingEnemyInThePlace());
    }

    private void createPooling()
    {
        MerkPooling.createPool(enRocket, 100);
    }

    public void playerDead()
    {
        if (life > 0) {
            bonusIcon.SetActive(false);
            bonusLengthText.text = "";
            StartCoroutine(PlayerBorn());
            Destroy(playerLifeIcon[life - 1]);
            playerLifeIcon.Remove(playerLifeIcon[life - 1]);
            life--;
        }
        else
        {
            onPlayerLive = false;
            //stageClear();
            showGameOver();
        }
    }

    private void stageClear()
    {
        Destroy(enemys);
    }

    private void showGameOver()
    {
        gameOverText.enabled = true;
    }

    IEnumerator movingEnemyInThePlace()
    {
        int _numb = enemys.transform.childCount - 1;
        for (int i = _numb; i >= 0; i--)
        {
            enemys.transform.GetChild(i).GetComponent<EnemyParamiters>().enemyMoving();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        enemys.GetComponent<EnemyMoveAll>().enemyShiftSpeed = 0.5f;
        yield break;
    }

    IEnumerator PlayerBorn()
    {
        Instantiate(playerBornAnimation, new Vector3(PLAYER_X + 0.15f, PLAYER_Y, playerPrefabs.transform.position.z), playerPrefabs.transform.rotation);
        for (int i = 0; i <= TIME_REBORN; i++)
        {
            if (i < TIME_REBORN)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                player = Instantiate(playerPrefabs, new Vector3(PLAYER_X, PLAYER_Y, playerPrefabs.transform.position.z), playerPrefabs.transform.rotation) as GameObject;
                PlayerController _pc = player.GetComponent<PlayerController>();
                _pc.bonusIcon = bonusIcon;
                _pc.bonusLengthText = bonusLengthText;
                yield break;
            }
        }
    }

    IEnumerator nextLevel()
    {
        Destroy(player);
        bonusIcon.SetActive(false);
        bonusLengthText.text = "";
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(3f);
        }
        Debug.Log("Next Level");
        level++;
        nextLevelText.text = "NEXT LEVEL IS " + level;
        levelText.text = "LEVEL: " + level;
        nextLevelText.enabled = true;
        StartCoroutine(nextLevelBorn());
        yield break;
    }

    IEnumerator nextLevelBorn()
    {
        for (byte i = 0; i < 1; i++)
        {
            
            yield return new WaitForSeconds(2f);
        }
        nextLevelText.enabled = false;
        StartCoroutine(liteSlider());
        enemysBorn();
        StartCoroutine(PlayerBorn());
        yield break;
    }

    IEnumerator liteSlider()
    {
        idif = 1f;
        for (byte i = 0; i < 20; i++)
        {
            idif -= 0.05f;
            slider.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, idif);
            yield return new WaitForSeconds(0.025f);
        }
        onPause = false;
        slider.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        yield break;
    }
    IEnumerator darkSlider()
    {
        idif = 0;
        for (byte i = 0; i < 20; i++)
        {
            idif += 0.05f;
            slider.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, idif);
            yield return new WaitForSeconds(0.025f);
        }
        //onPause = false;
        slider.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1f);
        yield break;
    }

    private void enemysBorn()
    {
        enemys = Instantiate(enemysPrefabs, new Vector3(ENEMY_X_POSITION, ENEMY_Y_POSITION, 2f), enemysPrefabs.transform.rotation) as GameObject;
        enemys.GetComponent<EnemyMoveAll>().enemyShiftSpeed = 0f;
        loadEnemy();
    }
}
