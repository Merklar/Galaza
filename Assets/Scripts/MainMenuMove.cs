using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MainMenuMove : MonoBehaviour {

	void Start () {
        gameObject.transform.DOMove(new Vector3(transform.position.x - 35, 0f, transform.position.z), 160f).SetRelative().SetLoops(-1, LoopType.Yoyo);
	}
}
