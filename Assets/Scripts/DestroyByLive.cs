using UnityEngine;
using System.Collections;

public class DestroyByLive : MonoBehaviour {

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name != "Player") {
              Destroy(other.gameObject);     
    }
    }
}
