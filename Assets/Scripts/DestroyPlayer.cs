using UnityEngine;
using System.Collections;

public class DestroyPlayer : MonoBehaviour {

    public GameObject explosion;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag != "Liver") && (other.tag != "PlayerRocket") && (other.tag != "Bonus"))
            {
                GameManager.Instance.playerDead();
                Instantiate(explosion, transform.position, transform.rotation);
                //MerkPooling.giveMerkPool(other.gameObject);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
}
