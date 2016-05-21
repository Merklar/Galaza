using UnityEngine;
using System.Collections;

public class TimerEnemyLaunch : MonoBehaviour {

    public GameObject rocket;
    public int FRAME_RATE;
    private float timer = 0;
    private int TIMER_LENGTH = 5;
    private float randValue = 0;
    public int LAUNCH_VER = 80;

	// Use this for initialization
	void Start () {
        //TIMER_LENGTH = Random.Range(3, 10);
	}
	
	// Update is called once per frame
	void Update () {
        if ((GameManager.Instance.onPlayerLive == true) && (GameManager.Instance.onPause == false))
        {
        if (++timer % (FRAME_RATE * TIMER_LENGTH) == 0)
        {
            timer = 0;
            randValue = Random.Range(0, 100);
            TIMER_LENGTH = Random.Range(3, 10);
            if (randValue > LAUNCH_VER)
            {
               // MerkPooling.takeMerkPool(transform.position, transform.rotation);
              Instantiate(rocket, transform.position, transform.rotation);
            }
        }
      }
	}
}
