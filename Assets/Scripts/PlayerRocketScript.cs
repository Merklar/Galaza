using UnityEngine;
using System.Collections;

public class PlayerRocketScript : MonoBehaviour {
    //-------------------------------------------------
    // PRIVATE CONSTANTS
    //-------------------------------------------------
    private const float SCREEN_TOP_SIDE = 5.0f;

    //-------------------------------------------------
    // PRIVATE VARIABLES
    //-------------------------------------------------
    public float rocketSpeed;
    public GameObject rocket;

    //-------------------------------------------------
    // PRIVATE VARIABLES
    //-------------------------------------------------
    private Vector2 rocketPos;
    private float transY;

	void Start () {
        GetComponent<Rigidbody2D>().velocity = transform.up * rocketSpeed; 
	}
	
}
