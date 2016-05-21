using UnityEngine;
using System.Collections;

public class MerkCollisionsDetector : MonoBehaviour {

    private static Transform transformTarget_1;
    private static Transform transformTarget_2;
    private static float demension;
    private static float radius;
    private static float demX;
    private static float demY;

	// Use this for initialization
	public static bool getCollision(GameObject target_1, GameObject target_2) {

        transformTarget_1 = target_1.transform;
        transformTarget_2 = target_2.transform;

        radius = (transformTarget_1.localScale.x / 2) + (transformTarget_2.localScale.x / 2);

        demX = transformTarget_2.position.x - transformTarget_2.position.x;
        demY = transformTarget_2.position.y - transformTarget_2.position.y;

        demension = Mathf.Sqrt(demX * demX + demY * demY);
        if (demension <= radius)
        {
            return true;
        }
        else
        {
            return false;
        }
	}
}
