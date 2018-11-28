using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManipulative : MonoBehaviour {

    public VRManipulation VRManip;
    //public ChangeColor ChangeColor;
    public GameObject Detector;

    private bool _pressed;
    private bool _collided;
    

    private GameObject _collidedObject;


    //left: -0.0352998 -0.003397927 0.07160564
    //right: 03699808 -0.00299 0.07200663

    // Use this for initialization
    void Start () {
        _pressed = false;
        _collided = false;
      
	}

    private void Update()
    {

        if (gameObject.tag == "leftController")
        {
            if (!OVRInput.Get(OVRInput.Button.Three) && _pressed)
            {
                ControllerUnpressed();
            }
        }
        else if (gameObject.tag == "rightController")
        {
            if (!OVRInput.Get(OVRInput.Button.One) && _pressed)
            {
                ControllerUnpressed();
            }
        }

       //zu sensibel --> delay? 
       if (Detector.GetComponent<Detector>().getSelectState() == true && _collided == false)
        {
            _collided = true;
            _collidedObject = Detector.GetComponent<Detector>().getCollidedObject();
            _collidedObject.GetComponent<ChangeColor>().changeColor();
        }
       else if (Detector.GetComponent<Detector>().getSelectState() == false)
        {
            _collided = false;
        }

        
    }

    private void ControllerUnpressed()
    {
        _pressed = false;
        VRManip.ControllerUnpressed(gameObject.tag);
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hallo");

        if (collision.gameObject.tag == "site")
        {
            ChangeColor.changeColor();
        }
    }
    */

    private void OnTriggerStay(Collider other)
    {
        
        if (gameObject.tag == "leftController")
        {
            if (OVRInput.Get(OVRInput.Button.Three) && !_pressed)
            {
                _pressed = true;
                VRManip.ControllerPressed(gameObject, gameObject.tag, other.gameObject);
            }
        }
        else if (gameObject.tag == "rightController")
        {
            if (OVRInput.Get(OVRInput.Button.One) && !_pressed)
            {
                _pressed = true;
                VRManip.ControllerPressed(gameObject, gameObject.tag, other.gameObject);
            }
        }
    }
}

