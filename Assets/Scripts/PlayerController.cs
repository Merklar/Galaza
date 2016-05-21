using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    //-------------------------------------------------
    //  GAMEOBJECT
    //-------------------------------------------------
    public GameObject bonusIcon;
    public Text bonusLengthText;
    //-------------------------------------------------
    // PRIVATE CONSTANTS
    //-------------------------------------------------
    private const float PLAYER_Y = -4.3f;
    private const float SCREEN_LEFT_SIDE = -5.9f;
    private const float SCREEN_RIGHT_SIDE = 5.9f;
    [SerializeField]
    private float RELOAD_TIME = 0.8f;
    [SerializeField]
    private float BONUS_RELOAD_TIME = 0.3f;
    [SerializeField]
    private byte BONUS_LENGTH_TIME = 12;

    //-------------------------------------------------
    // PUBLIC VARIABLES
    //-------------------------------------------------
    public float reloadShootTime = 0.8f;
    public float playerSpeed = 5.0f;
    public GameObject playerRocket;
    public float rocketSpeed;
    public bool onBonus = false;

    //-------------------------------------------------
    // PRIVATE VARIABLES
    //-------------------------------------------------
    private Vector2 playerPos = new Vector2(0, PLAYER_Y);
    private float transH;
    private bool mayShoot = false;
    private bool shoot = true;

	void Update () {
        transH = transform.position.x + (Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime);

        playerPos = new Vector2(Mathf.Clamp(transH, SCREEN_LEFT_SIDE, SCREEN_RIGHT_SIDE), PLAYER_Y);

        transform.position = playerPos;
        if (Input.GetKeyDown("space") == true)
        {
            //Debug.Log("press");
            //Instantiate(playerRocket, transform.position, transform.rotation);
            mayShoot = true;
        }
        if (Input.GetKeyUp("space") == true)
        {
            mayShoot = false;
           // shoot = true;
        }
        if (mayShoot == true)
        {
            if (shoot == true)
            {
                Instantiate(playerRocket, transform.position, transform.rotation);
                shoot = false;
                StartCoroutine(reloadShoot());
            }
        }
    }

    IEnumerator reloadShoot()
    {
        for (byte i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(reloadShootTime);
        }
        shoot = true;
        yield break;
    }

    public void GetSpeedShootBonus()
    {
        onBonus = true;
        Debug.Log("Speed shoot start");
        reloadShootTime = BONUS_RELOAD_TIME;
        StartCoroutine(bonusTimer());
        bonusIcon.SetActive(true);
        bonusLengthText.text = "TIME: " + BONUS_LENGTH_TIME;
    }

    IEnumerator bonusTimer()
    {
        {
            for (byte i = 0; i < BONUS_LENGTH_TIME; i++)
            {
                yield return new WaitForSeconds(1);
                bonusLengthText.text = "TIME: " + (BONUS_LENGTH_TIME - i - 1);
            }
            reloadShootTime = RELOAD_TIME;
            bonusIcon.SetActive(false);
            bonusLengthText.text = "";
            onBonus = false;
            Debug.Log("Speed shoot stop");
            yield break;
        }
    }
}
