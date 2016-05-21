using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{

    public GameObject explosion;
    public GameObject bonusSpeedShootPrefabs;
    public int ENEMY_COINS;

    [SerializeField]
    private  float BONUS_VER = 100;
    private float tempBonusVer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag != "Enemy") && (other.tag != "Liver") && (other.tag != "Explosion") && (other.tag != "Bonus"))
            {
                ShieldComponent shield = gameObject.GetComponent<ShieldComponent>();
                if (shield != null)
                {
                    if (shield.shieldLife <= 0)
                    {
                        GameManager.Instance.setChangeScore(ENEMY_COINS);
                        Instantiate(explosion, transform.position, transform.rotation);
                        Destroy(other.gameObject);
                        Destroy(gameObject);
                    }
                    else
                    {
                        shield.shieldLife--;
                        Destroy(other.gameObject);
                    }
                }
                else
                {
                    tempBonusVer = Random.Range(0f, 100f);
                    if (BONUS_VER >= tempBonusVer)
                    {
                        Instantiate(bonusSpeedShootPrefabs, transform.position, transform.rotation);
                        Debug.Log("Bonus");
                    }
                    GameManager.Instance.setChangeScore(ENEMY_COINS);
                    Instantiate(explosion, transform.position, transform.rotation);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }              
    }
}
