using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManipulative : MonoBehaviour {

    public VRManipulation VRManip;

    private bool _pressed;
	// Use this for initialization
	void Start () {
        _pressed = false;
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
    }

    private void ControllerUnpressed()
    {
        _pressed = false;
        VRManip.ControllerUnpressed(gameObject.tag);
    }

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

