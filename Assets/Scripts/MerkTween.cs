using UnityEngine;
using System.Collections;
using DG.Tweening;

public  class MerkTween : MonoBehaviour {

    private static bool onCircleTween = false;
    private static GameObject _target;
    //private static CircleTween tween;

	// Use this for initialization
	void  Start () {
        Debug.Log("Starting Merk Tween");
	}

    public static void Move(GameObject aTarget, float aDuration, Vector3 aPoint)
    {
        aTarget.transform.DOMove(aPoint, aDuration);
    }

    public static void MoveCircle(GameObject aTarget)
    {
        _target = aTarget;
        CircleTween _tween = _target.AddComponent<CircleTween>();
        //CircleTween _tween = new CircleTween();
        _tween.TweenCircle(_target);
        onCircleTween = true;
    }

    void Update()
    {
        if (onCircleTween == true)
        {
            Debug.Log("Circle");
            float t = Time.time;
            _target.transform.position = new Vector3(_target.transform.position.x + Mathf.Sin(t), _target.transform.position.y + Mathf.Cos(t), _target.transform.position.z);
        }
    }
}
