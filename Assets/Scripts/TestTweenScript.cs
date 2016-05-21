using UnityEngine;
using System.Collections;

public class TestTweenScript : MonoBehaviour {

    private Vector3 _vect = new Vector3(1, 2, 2);

	void Start () {
        MerkTween.MoveCircle(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
