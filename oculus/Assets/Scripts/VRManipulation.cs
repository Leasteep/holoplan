using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// collider Player ? 
public class VRManipulation : MonoBehaviour {

    
    public float MoveStrength = 1;
    public float ScaleStrength = 1;

   
    private GameObject _leftController;
    private GameObject _rightController;

    private GameObject _leftControllerManipObject;
    private GameObject _rightControllerManipObject;
    private GameObject _mapAnchor;

    private bool _leftControllerPressed;
    private bool _rightControllerPressed;

    private Vector3 _lastLeftControllerPos;
    private Vector3 _lastRightControllerPos;

    private Vector3 _lastDistanceBetweenController;
    private Vector3 _lastPositionAnchor;

    // Use this for initialization
    void Start () {

       // Physics.IgnoreCollision(control, _leftControllerManipObject);
        _leftControllerPressed = false;
        _rightControllerPressed = false;
        _mapAnchor = GameObject.Find("/MapAnchor");
    }
	
	// Update is called once per frame
	void Update (){
        
        
        if (_leftControllerPressed && !_rightControllerPressed)
        {
            Vector3 controllerPosDelta = _lastLeftControllerPos - _leftController.transform.position;

            _leftControllerManipObject.transform.position -= MoveStrength * controllerPosDelta;

            _lastLeftControllerPos = _leftController.transform.position;
        }
        else if(!_leftControllerPressed && _rightControllerPressed)
        {
            Vector3 controllerPosDelta = _lastRightControllerPos - _rightController.transform.position;
            
            _rightControllerManipObject.transform.position -= MoveStrength * controllerPosDelta;

            _lastRightControllerPos = _rightController.transform.position;
        }
        else if(_leftControllerPressed && _rightControllerPressed)
        {
            if(_leftControllerManipObject == _rightControllerManipObject)
            {
                Vector3 DistanceBetweenController = _leftController.transform.position - _rightController.transform.position;
                float scale = _lastDistanceBetweenController.magnitude - DistanceBetweenController.magnitude;
                float rot = Vector3.SignedAngle(_lastDistanceBetweenController.normalized, DistanceBetweenController.normalized, Vector3.up);

 /*
                
                Vector3 LeftcontrollerPosDelta = _lastLeftControllerPos - _leftController.transform.position;
                Vector3 RightcontrollerPosDelta = _lastRightControllerPos - _rightController.transform.position;

                float xPos;
                float zPos;
                if((LeftcontrollerPosDelta.x < 0) ^ (RightcontrollerPosDelta.x < 0))
                {
                    xPos = LeftcontrollerPosDelta.x + RightcontrollerPosDelta.x;
                }
                else
                {
                    xPos = Mathf.Abs(LeftcontrollerPosDelta.x - RightcontrollerPosDelta.x);
                }

                if ((LeftcontrollerPosDelta.z < 0) ^ (RightcontrollerPosDelta.z < 0))
                {
                    zPos = LeftcontrollerPosDelta.z + RightcontrollerPosDelta.z;
                }
                else
                {
                    zPos = Mathf.Abs(LeftcontrollerPosDelta.z - RightcontrollerPosDelta.z);
                }

                _lastLeftControllerPos = _leftController.transform.position;
                _lastRightControllerPos = _rightController.transform.position;
                _leftControllerManipObject.transform.position -= new Vector3(xPos, 0, zPos);

             */   

                // für rot und scale, map anchor auf die pos zwischen den beiden controllern bewegen, dabei die map (child) zurückbewegen (negative bewegung wie die anchor bewegung) und erst dann den Anchor rotieren/skalieren
                // set map anchor on position 

                _mapAnchor.transform.position = _leftController.transform.position + DistanceBetweenController/2;
                 Vector3 MovementAnchor = _lastPositionAnchor - _mapAnchor.transform.position;
                _leftControllerManipObject.transform.position += MovementAnchor;

                _mapAnchor.transform.rotation = _mapAnchor.transform.rotation * Quaternion.Euler(0, rot, 0);
                _mapAnchor.transform.localScale -= new Vector3(1, 0, 1) * scale * ScaleStrength;
                _lastDistanceBetweenController = DistanceBetweenController;
                //_lastLeftControllerPos = _leftController.transform.position;
                _lastPositionAnchor = _mapAnchor.transform.position;
            }
        }
        
	}

    public void ControllerPressed(GameObject controller, string ControllerTag, GameObject manipulatedObject)
    {
        if(ControllerTag == "leftController")
        {
            _leftController = controller;
            _leftControllerManipObject = manipulatedObject;
            _leftControllerPressed = true;
            _lastLeftControllerPos = controller.transform.position;
            
        }
        else if(ControllerTag == "rightController")
        {
            _rightController = controller;
            _rightControllerManipObject = manipulatedObject;
            _rightControllerPressed = true;
            _lastRightControllerPos = controller.transform.position;
        }
        if(_leftControllerPressed && _rightControllerPressed)
        {
            _lastDistanceBetweenController = _leftController.transform.position - _rightController.transform.position;
            _lastPositionAnchor = _mapAnchor.transform.position;
        }
    }

    public void ControllerUnpressed(string ControllerTag)
    {
        if (ControllerTag == "leftController")
        {
            _leftControllerPressed = false;
        }
        else if (ControllerTag == "rightController")
        {
            _rightControllerPressed = false;
        }
    }
}
