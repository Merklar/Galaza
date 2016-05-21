using UnityEngine;
using System.Collections;

public class SpeedShootBonusScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            PlayerController _pc = other.GetComponent<PlayerController>();
            if (_pc.onBonus == false)
            {
            Debug.Log("Player get shoot speed bonus");
            _pc.GetSpeedShootBonus();
            Destroy(gameObject);
            } else {
            Destroy(gameObject);
            }
        }
    }
}
