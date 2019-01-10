using System.Collections;
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

        if (obj.gameObject.tag == "site")
        {
           
                _collision = true;
                _collidedObject = obj.gameObject;
         
        }
    }


    private void OnTriggerExit(Collider other)
    {

        Delay();
        _collision = false;
    }


    public bool getCollisionState()
    {
        return _collision;
    }

    public GameObject getCollidedObject()
    {
        return _collidedObject;
    }

   IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
    }
    
}

