using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    private bool _selected;
    private GameObject _collidedObject;
    
	// Use this for initialization
	void Start () {

        _selected = false;
	}

    /* Update is called once per frame
    private void Update()
    {
        _selected = false;
    }
    */
    private void OnTriggerEnter (Collider obj)
    {
       
        if (obj.gameObject.tag == "side")
        {
            Debug.Log("hello");
            _selected = true;
            _collidedObject = obj.gameObject;
        
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        _selected = false;
    }

    public bool getSelectState()
    {
        return _selected;
    }

    public GameObject getCollidedObject()
    {
        return _collidedObject;
    }
}
