using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is the main script, it calculates the map manipulation depending on the interactions with both controllers
 * It allows to move, scale and rotate the maps  
 */
public class VRManipulation : MonoBehaviour
{
    //factors to calibrate the move and scale strength, they can be modified in the inspector if required
    public float MoveStrength = 1;
    public float ScaleStrength = 1;

    //used to allow to manipulate all maps at once (maps are children of the parentMap)
    public GameObject ParentMap;
    //used to activate/deactivate the possibility to select objects with the right or left index finger 
    //(see detctor script for more details)
    public GameObject RightDetector;
    public GameObject LeftDetector;

    private GameObject _leftController;
    private GameObject _rightController;
    // represents the object currently manipulated by one of the controllers
    private GameObject _leftControllerManipObject;
    private GameObject _rightControllerManipObject;
    // the function of the mapAnchor is to rotate and scale around a specific point dependent on the controller position 
    private GameObject _mapAnchor;
    private bool _leftControllerPressed;
    private bool _rightControllerPressed;
    // used to calculate the transformation for each frame
    private Vector3 _lastLeftControllerPos;
    private Vector3 _lastRightControllerPos;
    private Vector3 _lastDistanceBetweenController;
    private Vector3 _lastPositionAnchor;
    private Vector3 _lastPositionManipObject;

    /* initialize so that the left and right controllers are not pressed 
     * and the mapAnchor variable is set to our mapAnchor object in the application
     */
    void Start()
    {
        _leftControllerPressed = false;
        _rightControllerPressed = false;
        _mapAnchor = GameObject.Find("/MapAnchor");
    }

    /* this method is called every frame and checks if one or both cotrollers are pressed
     * with one hand you can move the ManipObject by the real movement (= controllerPosDelta) * the factor of the MoveStrength
     * with both hands you can scale, rotate and move the ManipObject
     * in general we allow movements at all axis, rotation around the y axis and scaling on the x and z axis 
     */
    void Update()
    {
        oneHandManipulation();
        twoHandsManipulation();
    }

    private void twoHandsManipulation()
    {
        //manipulations with both hands (movement, rotation, scale) 
        if (_leftControllerPressed && _rightControllerPressed)
        {
            //checks if we touch the same object with both hands 
            if (_leftControllerManipObject == _rightControllerManipObject)
            {
                Vector3 DistanceBetweenController = new Vector3();
                float scale;
                float rot;
                float xPos;
                float zPos;
                float yPos;

                calculation(out DistanceBetweenController, out scale, out rot, out xPos, out zPos, out yPos);

                moveWithBothHands(xPos, zPos, yPos);

                rotation(DistanceBetweenController, rot);

                scaling(scale);

                //save the current data as last data for the next calculation 
                _lastLeftControllerPos = _leftController.transform.position;
                _lastRightControllerPos = _rightController.transform.position;
                _lastDistanceBetweenController = DistanceBetweenController;
                _lastPositionAnchor = _mapAnchor.transform.position;
                _lastPositionManipObject = _leftControllerManipObject.transform.position;
            }

        }
    }

    /* scales the map anchor, which also scales the three child maps
     */
    private void scaling(float scale)
    {
        //scale boundaries to prevent scaling the maps too small
        if (_mapAnchor.transform.localScale.x < 0.5f && _mapAnchor.transform.localScale.z < 0.5f)
        {
            _mapAnchor.transform.localScale = new Vector3(0.5f, _mapAnchor.transform.localScale.y, 0.5f);
        }
        //scale (x and z axis) 
        _mapAnchor.transform.localScale -= new Vector3(1, 0, 1) * scale * ScaleStrength;
    }

    /* rotate the object with the mapAnchor 
        * we work with an mapAnchor to rotate the map around a midpoint between both controllers
        * if we would't use this trick we would rotate the maps at one corner and away from the controllers
        * we place the mapAnchor at this midpoint and because we dont wan't to move the maps (child of the mapAnchor) we also push our maps back to the old position 
        */
    private void rotation(Vector3 DistanceBetweenController, float rot)
    {
        _mapAnchor.transform.position = new Vector3
            (_leftController.transform.position.x + DistanceBetweenController.magnitude / 2,
             _lastPositionManipObject.y,
             _leftController.transform.position.z + DistanceBetweenController.magnitude / 2);
        //move back the map
        _leftControllerManipObject.transform.position = _lastPositionManipObject;

        //rotate (around the y axis)
        _mapAnchor.transform.rotation = _mapAnchor.transform.rotation * Quaternion.Euler(0, rot, 0);
    }

    private void moveWithBothHands(float xPos, float zPos, float yPos)
    {
        //move the ManipObject and save the new position
        _leftControllerManipObject.transform.position -= new Vector3(xPos, yPos, zPos) * MoveStrength;
        _lastPositionManipObject = _leftControllerManipObject.transform.position;
    }

    private void calculation(out Vector3 DistanceBetweenController, out float scale, out float rot, out float xPos, out float zPos, out float yPos)
    {
        // calculate the current distance between the two controllers 
        DistanceBetweenController = _leftController.transform.position - _rightController.transform.position;

        // calculate the length of last distance between the controller and the length of the current distance
        scale = _lastDistanceBetweenController.magnitude - DistanceBetweenController.magnitude;

        // calculate the angle between the old distanceVector (normalized) and the new distanceVector (normalized) and the y-axis
        rot = Vector3.SignedAngle(_lastDistanceBetweenController.normalized, DistanceBetweenController.normalized, Vector3.up);

        // calculate the movement vector of the controller 
        Vector3 LeftcontrollerPosDelta = _lastLeftControllerPos - _leftController.transform.position;
        Vector3 RightcontrollerPosDelta = _lastRightControllerPos - _rightController.transform.position;


        /* move the object with both hands
         * checks if both controllers move in the same direction in the x,y and z axis, 
         * if yes we calculate the average of the movement, 
         * if no we use the position data from the left controller 
         * we set our boundary to 1 to detect quite exact movements, but to eliminate natural position changes, which occur continuously in a minimal range
         */
        if ((LeftcontrollerPosDelta.x < 1) && (RightcontrollerPosDelta.x < 1) || (LeftcontrollerPosDelta.x > 1) && (RightcontrollerPosDelta.x > 1))
        {
            xPos = (LeftcontrollerPosDelta.x + RightcontrollerPosDelta.x) / 2;
        }
        else
        {
            xPos = _leftControllerManipObject.transform.position.x;
        }
        if ((LeftcontrollerPosDelta.z < 1) && (RightcontrollerPosDelta.z < 1) || (LeftcontrollerPosDelta.z > 1) && (RightcontrollerPosDelta.z > 1))
        {
            zPos = (LeftcontrollerPosDelta.z + RightcontrollerPosDelta.z) / 2;
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
    }

    private void oneHandManipulation()
    {
        //move object with the left hand
        if (_leftControllerPressed && !_rightControllerPressed)
        {
            //Calculate the movement by the last controller position - the current position    
            Vector3 controllerPosDelta = _lastLeftControllerPos - _leftController.transform.position;
            //manipulate the object 
            _leftControllerManipObject.transform.position -= MoveStrength * controllerPosDelta;
            //set the current position as last controller position 
            _lastLeftControllerPos = _leftController.transform.position;
        }

        //move object with the right hand (same as left hand)
        else if (!_leftControllerPressed && _rightControllerPressed)
        {
            Vector3 controllerPosDelta = _lastRightControllerPos - _rightController.transform.position;
            _rightControllerManipObject.transform.position -= MoveStrength * controllerPosDelta;
            _lastRightControllerPos = _rightController.transform.position;
        }
    }

    /* method is called in the VR Manipulative script (each controller has his own script instance)
     * instantiate the calling controller 
     * set _last* variables to calculate the next movement depending on the start position
     * inactivate the detectorObjects to prevent collisions while manipulating the maps
     */
    public void ControllerPressed(GameObject controller, string ControllerTag, GameObject manipulatedObject)
    {
        if (ControllerTag == "leftController")
        {
            LeftDetector.SetActive(false);
            _leftController = controller;
            _leftControllerManipObject = ParentMap;
            _leftControllerPressed = true;
            _lastLeftControllerPos = controller.transform.position;

        }
        else if (ControllerTag == "rightController")
        {
            RightDetector.SetActive(false);
            _rightController = controller;
            _rightControllerManipObject = ParentMap;
            _rightControllerPressed = true;
            _lastRightControllerPos = controller.transform.position;
        }

        if (_leftControllerPressed && _rightControllerPressed)
        {
            _lastDistanceBetweenController = _leftController.transform.position - _rightController.transform.position;
            _lastPositionAnchor = new Vector3(_mapAnchor.transform.position.x, manipulatedObject.transform.position.y, _mapAnchor.transform.position.z);
            _lastPositionManipObject = manipulatedObject.transform.position;
        }
    }

    /* method is called in the VR Manipulative script  (each controller has his own script instance)
     * set the pressed variable on false so the update function is not changing data anymore
     * activate the detectorObjects to allow selecting objects
     */
    public void ControllerUnpressed(string ControllerTag)
    {
        if (ControllerTag == "leftController")
        {
            _leftControllerPressed = false;
            LeftDetector.SetActive(true);
        }
        else if (ControllerTag == "rightController")
        {
            _rightControllerPressed = false;
            RightDetector.SetActive(true);
        }
    }
}
