using UnityEngine;
using System.Collections;

public class EnemyMoveAll : MonoBehaviour
{

    //-------------------------------------------------
    // PRIVATE CONSTANTS
    //-------------------------------------------------
    private const float SCREEN_LEFT_SIDE = -0.9f;
    private const float SCREEN_RIGHT_SIDE = 0.9f;

    //-------------------------------------------------
    // PUBLIC VARIABLES
    //-------------------------------------------------
    public float enemyShiftSpeed;

    //-------------------------------------------------
    // PRIVATE VARIABLES
    //-------------------------------------------------
    private bool onRight = true;
    private Vector2 enemyPos;
    private float transX;


    // Update is called once per frame
    void Update()
    {
            if (transform.position.x > SCREEN_RIGHT_SIDE)
            {
                onRight = false;
            }
            if (transform.position.x < SCREEN_LEFT_SIDE)
            {
                onRight = true;
            }
            if (onRight == true)
            {
                transX = transform.position.x + (enemyShiftSpeed * Time.deltaTime);
                enemyPos = new Vector2(transX, transform.position.y);
                transform.position = enemyPos;
            }
            else
            {
                transX = transform.position.x - (enemyShiftSpeed * Time.deltaTime);
                enemyPos = new Vector2(transX, transform.position.y);
                transform.position = enemyPos;
            }
        }
}