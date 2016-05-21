using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MainMenuShepRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.transform.DORotate(new Vector3(transform.position.x, transform.position.y, transform.position.z + 15f), 6f).SetRelative().SetLoops(-1, LoopType.Yoyo);
	}
	
	// Update is called once per frame

}
