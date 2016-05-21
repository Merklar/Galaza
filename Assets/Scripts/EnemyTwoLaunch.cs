using UnityEngine;
using System.Collections;

public class EnemyTwoLaunch : MonoBehaviour
{

    #region GameObject
    public GameObject motherPrefab;
    public GameObject motherPrefab2;
    #endregion

    #region Varriable
    private Vector3 MOTHER_POSITION;
    private float timer = 0;
    private int TIMER_LENGTH;
    private float randValue = 0;
    public int FRAME_RATE = 60;
    public int LAUNCH_VER = 80;
    private float tempPositionX;
    private float tempPositionY;
    private bool onFly = false;
    private string state = STATE_SIMPLE;
    #endregion

    #region Constant
    private const string STATE_SIMPLE = "1";
    private const string STATE_FLY = "2";
    #endregion

    // Use this for initialization
	void Start () {
        TIMER_LENGTH = Random.Range(1, 5);
        MOTHER_POSITION = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if ((onFly == false) && (GameManager.Instance.onPlayerLive == true) && (GameManager.Instance.onPause == false))
        {
            if (++timer % (FRAME_RATE * TIMER_LENGTH) == 0)
            {
                timer = 0;
                randValue = Random.Range(0, 100);
                TIMER_LENGTH = Random.Range(5, 15);
                if (randValue > LAUNCH_VER)
                {
                    state = STATE_FLY;
                   // MOTHER_POSITION = gameObject.transform.position;
                    transform.parent = null;
                    flyToBot();
                }
            }
        }
	}

    private void flyToBot()
    {
        //iTween.MoveBy(gameObject, iTween.Hash("y", -6f, "time", 4f));
        
    }
}
