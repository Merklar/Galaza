using UnityEngine;
using System.Collections;

public class CircleTween : MonoBehaviour
{
    private  bool onCircleTween = false;
    private GameObject _target;
	// Use this for initialization
    void Start()
    {
       
	}

    public void TweenCircle(GameObject aTrager)
    {
        _target = aTrager;
        Debug.Log("Start MoveCircle");
        onCircleTween = true;
    }
	
	// Update is called once per frame
    void Update()
    {
        if (onCircleTween == true)
        {
            float t = Time.time;
           
            _target.transform.position = new Vector3(_target.transform.position.x + Mathf.Sin(t)/20, _target.transform.position.y + Mathf.Cos(t)/20, _target.transform.position.z);
            //_target.transform.rotation = new
        }
    }
}
