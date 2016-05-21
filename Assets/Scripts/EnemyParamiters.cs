using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyParamiters : MonoBehaviour {

    public Vector3 tempPosition;
    public Vector3 centralPosition;

    public void enemyMoving()
    {
        transform.DOMove(tempPosition, 2f);
    }
}
