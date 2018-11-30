using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    private bool _collision;
    private GameObject _collidedObject;
    
	// Use this for initialization
	void Start () {

        _collision = false;
	}

    /* Update is called once per frame
    private void Update()
    {
        _selected = false;
    }
    */
    private void OnTriggerEnter (Collider obj)
    {
       
        if (obj.gameObject.tag == "site") //ToDo: Tags müssen an Connectetegruppen von sites angepasst werden 
        {
            _collision = true;
            _collidedObject = obj.gameObject;
        
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        _collision = false;
    }

    public bool getSelectState()
    {
        return _collision;
    }

    public GameObject getCollidedObject()
    {
        return _collidedObject;
    }
}
