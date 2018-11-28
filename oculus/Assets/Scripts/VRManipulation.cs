using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// collider Player ? 
public class VRManipulation : MonoBehaviour {

    
    public float MoveStrength = 1;
    public float ScaleStrength = 1;

    public GameObject Map;
   
    private GameObject _leftController;
    private GameObject _rightController;

    private GameObject _leftControllerManipObject;
    private GameObject _rightControllerManipObject;
    private GameObject _mapAnchor;
    private GameObject _collidedSite;

    private bool _leftControllerPressed;
    private bool _rightControllerPressed;

    private Vector3 _lastLeftControllerPos;
    private Vector3 _lastRightControllerPos;

    private Vector3 _lastDistanceBetweenController;
    private Vector3 _lastPositionAnchor;
    private Vector3 _lastPositionManipObject;

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
            // move object with the leftHand
            Vector3 controllerPosDelta = _lastLeftControllerPos - _leftController.transform.position;

            _leftControllerManipObject.transform.position -= MoveStrength * controllerPosDelta;

            _lastLeftControllerPos = _leftController.transform.position;
        }
        else if(!_leftControllerPressed && _rightControllerPressed)
        {
            //move object with the rightHand
            Vector3 controllerPosDelta = _lastRightControllerPos - _rightController.transform.position;
            
            _rightControllerManipObject.transform.position -= MoveStrength * controllerPosDelta;

            _lastRightControllerPos = _rightController.transform.position;
        }
        else if(_leftControllerPressed && _rightControllerPressed)
        {
            if(_leftControllerManipObject == _rightControllerManipObject)
            {
                // calculate the distance between the two controller 
                Vector3 DistanceBetweenController = _leftController.transform.position - _rightController.transform.position;
                // calculate the length of last distance between the controller and the length of the actual distance = sacle factor
                float scale = _lastDistanceBetweenController.magnitude - DistanceBetweenController.magnitude;
                // calculate the angle between the old distanceVec (normalized) and the new distanceVec(normalized) and the Axis (y)
                float rot = Vector3.SignedAngle(_lastDistanceBetweenController.normalized, DistanceBetweenController.normalized, Vector3.up);

                

                               Vector3 LeftcontrollerPosDelta = _lastLeftControllerPos - _leftController.transform.position;
                               Vector3 RightcontrollerPosDelta = _lastRightControllerPos - _rightController.transform.position;

                               float xPos;
                               float zPos;
                               float yPos;
                               if((LeftcontrollerPosDelta.x < 1) && (RightcontrollerPosDelta.x < 1) || (LeftcontrollerPosDelta.x > 1) && (RightcontrollerPosDelta.x > 1))
                               {
                                   xPos = (LeftcontrollerPosDelta.x + RightcontrollerPosDelta.x)/2;
                               }
                               else
                               {
                                   xPos = _leftControllerManipObject.transform.position.x;
                               }

                               if ((LeftcontrollerPosDelta.z < 1) && (RightcontrollerPosDelta.z < 1) || (LeftcontrollerPosDelta.z > 1) && (RightcontrollerPosDelta.z > 1))
                               {
                                   zPos = (LeftcontrollerPosDelta.z + RightcontrollerPosDelta.z)/2;
                               }
                               else
                               {
                                    zPos = _leftControllerManipObject.transform.position.z;
                               }
                               if ((LeftcontrollerPosDelta.y < 1) && (RightcontrollerPosDelta.y < 1) || (LeftcontrollerPosDelta.y > 1) && (RightcontrollerPosDelta.y > 1))
                               {
                                    yPos = (LeftcontrollerPosDelta.y + RightcontrollerPosDelta.y) / 2;
                               }
                               else
                               {
                                    yPos = _leftControllerManipObject.transform.position.y;
                               }

                _lastLeftControllerPos = _leftController.transform.position;
                _lastRightControllerPos = _rightController.transform.position;
                _leftControllerManipObject.transform.position -= new Vector3(xPos, yPos, zPos) * MoveStrength;
                _lastPositionManipObject = _leftControllerManipObject.transform.position;

                            

                // für rot und scale, map anchor auf die pos zwischen den beiden controllern bewegen, dabei die map (child) zurückbewegen (negative bewegung wie die anchor bewegung) und erst dann den Anchor rotieren/skalieren
                // set map anchor on position 
                
                _mapAnchor.transform.position = new Vector3(_leftController.transform.position.x + DistanceBetweenController.magnitude/2 ,
                _lastPositionManipObject.y ,_leftController.transform.position.z + DistanceBetweenController.magnitude / 2) ;
                 Vector3 movementAnchor = _lastPositionAnchor - _mapAnchor.transform.position;
                _leftControllerManipObject.transform.position = _lastPositionManipObject ;

                _mapAnchor.transform.rotation = _mapAnchor.transform.rotation * Quaternion.Euler(0, rot, 0);
                
                //scaleboundaries 
                if (_mapAnchor.transform.localScale.x < 0.5f && _mapAnchor.transform.localScale.z < 0.5f)
                {
                    _mapAnchor.transform.localScale = new Vector3(0.5f, _mapAnchor.transform.localScale.y, 0.5f);
                }
                _mapAnchor.transform.localScale -= new Vector3(1, 0, 1) * scale * ScaleStrength;

                _lastDistanceBetweenController = DistanceBetweenController;
                _lastPositionAnchor = _mapAnchor.transform.position;
                _lastPositionManipObject = _leftControllerManipObject.transform.position;
            }
        }
        
	}

    public void ControllerPressed(GameObject controller, string ControllerTag, GameObject manipulatedObject)
    {
        if(ControllerTag == "leftController")
        {
            _leftController = controller;
            _leftControllerManipObject = manipulatedObject;
           // this.GetComponent<DefineChildren>().setChildren(_leftControllerManipObject);
            _leftControllerPressed = true;
            _lastLeftControllerPos = controller.transform.position;
            
        }
        else if(ControllerTag == "rightController")
        {
            _rightController = controller;
            _rightControllerManipObject = manipulatedObject;
            //this.GetComponent<DefineChildren>().setChildren(_rightControllerManipObject);
            _rightControllerPressed = true;
            _lastRightControllerPos = controller.transform.position;
        }
        if(_leftControllerPressed && _rightControllerPressed)
        {
            _lastDistanceBetweenController = _leftController.transform.position - _rightController.transform.position;
            _lastPositionAnchor = new Vector3(_mapAnchor.transform.position.x, manipulatedObject.transform.position.y, _mapAnchor.transform.position.z);
            _lastPositionManipObject = manipulatedObject.transform.position;
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

    public void selectSite(GameObject site)
    {
        site.GetComponent<ChangeColor>().changeColor();
        this.GetComponent<DefineChildren>().setConnection();
       
    }

}
