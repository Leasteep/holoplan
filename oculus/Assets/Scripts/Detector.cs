﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    private bool _collision;
    private GameObject _collidedObject;

    // Use this for initialization
    void Start() {

        _collision = false;
    }

    /* Update is called once per frame
    private void Update()
    {
        _selected = false;
    }
    */
    private void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Hall0");

        if (obj.gameObject.tag == "site")
        {
           if (_collision == false)
            {
                _collision = true;
                _collidedObject = obj.gameObject;
           }
           else if (_collision == true)
            {
                _collision = false;
            }


        }
    }


   /* private void OnTriggerExit(Collider other)
    {
        _collision = false;
    }

*/
    public bool getCollisionState()
    {
        return _collision;
    }

    public GameObject getCollidedObject()
    {
        return _collidedObject;
    }
}

