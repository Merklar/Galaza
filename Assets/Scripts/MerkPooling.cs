using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MerkPooling : MonoBehaviour {

    #region Variable
    private static GameObject _target;
    private static int _numb;
    private static List<GameObject> mainPool = new List<GameObject>();
    #endregion

	// Use this for initialization
    public static void  createPool(GameObject target, int numb)
    {
        _target = target;
        _numb = numb;
        for (int i = 0; i < _numb; i++)
        {
            mainPool.Add(Instantiate(_target) as GameObject);
            mainPool[i].SetActive(false);
        }
        //return mainPool;
	}

    public static void takeMerkPool(Vector3 needPosition, Quaternion needRotation)
    {
        GameObject _tempTarget = null;
        Vector3 _needPosition = needPosition;
        Quaternion _needRotation = needRotation;
        int _MPcount = mainPool.Count;
        for (byte i = 0; i < _MPcount; i++)
        {
            if (!mainPool[i].activeInHierarchy)
            {
                _tempTarget = mainPool[i];
                _tempTarget.transform.position = _needPosition;
                _tempTarget.transform.rotation = _needRotation;
                _tempTarget.SetActive(true);
                break;
            }
        }
    }

    public static void giveMerkPool(GameObject giveTarget)
    {
        GameObject _giveTarget = giveTarget;
        giveTarget.SetActive(false);
        mainPool.Add(_giveTarget);
    }
}
