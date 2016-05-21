using UnityEngine;
using System.Collections;

public class EnemyRocketScript : MonoBehaviour {

    //-------------------------------------------------
    // PRIVATE VARIABLES
    //-------------------------------------------------
    public float rocketSpeed;
    //public GameObject rocket;

    //-------------------------------------------------
    // PRIVATE VARIABLES
    //-------------------------------------------------
    //private Vector2 rocketPos;
    //private float transY;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * -rocketSpeed;
    }
	
}
