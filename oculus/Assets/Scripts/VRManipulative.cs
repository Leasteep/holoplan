using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManipulative : MonoBehaviour {

    public VRManipulation VRManip;
    public ActivateSites ActivateSites;
    public GameObject Detector;

    private bool _pressed;
    private bool _collided;
    private GameObject _site;

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
            if (!((OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)) > 0.8) && _pressed) //wenn der Trigger nicht mehr gedrückt ist 
            {
                ControllerUnpressed();
            }
        }
        else if (gameObject.tag == "rightController")
        {
            if (!((OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)) > 0.8) && _pressed)
            {
                ControllerUnpressed();
            }
        }

       if (Detector.GetComponent<Detector>().getCollisionState() == true && _collided == false) //wenn der Block am Finger collidiert und vorher noch nicht collidiert ist 
        {
            
            _site = Detector.GetComponent<Detector>().getCollidedObject();
            if (_site.GetComponent<Site>().getSelectionState() == false)
            {
                Debug.Log("aktive");
                ActivateSites.setActive(_site);
            }
            else
            {
                ActivateSites.setInactive(_site);
            }
            _collided = true;


        }
       else if (Detector.GetComponent<Detector>().getCollisionState() == false && _collided == true)
        {
            Debug.Log("inaktive");
            if (_site.GetComponent<Site>().getSelectionState() == true)
            {
                ActivateSites.setInactive(_site);
            }
            else
            {
                ActivateSites.setActive(_site);
            }
            _collided = false;
        }

        
    }

    private void ControllerUnpressed()
    {
        _pressed = false;
        VRManip.ControllerUnpressed(gameObject.tag);
    }



    private void OnTriggerStay(Collider other) //solange der Trigger gedrückt wird 
    {
        
        if (gameObject.tag == "leftController")
        {
            if ((OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)) > 0.8 && !_pressed) //>0.8 damit der Trigger erst als gedrückt gilt wenn er weiter reingedrückt wurde und nicht sobald er nur leicht berührt wird 
            {
                _pressed = true;
                VRManip.ControllerPressed(gameObject, gameObject.tag, other.gameObject);
            }
        }
        else if (gameObject.tag == "rightController")
        {
            if ((OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)) > 0.8 && !_pressed)
            {
                _pressed = true;
                VRManip.ControllerPressed(gameObject, gameObject.tag, other.gameObject);
            }
        }
    }

    
}

