using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Each controller has his own script instance (so we have to distinguish between the two controllers by their tags)
 * We use this script to detect if the trigger is pressed, if an object is selcted and which one (by using the detector)
 */
public class VRManipulative : MonoBehaviour
{
    // to allow interaction with the maps
    public VRManipulation VRManip;
    // to allow activating sites
    public ActivateSites ActivateSites;
    // to detect collisions with sites (by a small cube at the index finger of each hand avatar in the application)
    public GameObject Detector;
    // for force feedback
    public AudioClip HapticAudioClip;

    private bool _pressed;
    private bool _collided;
    private GameObject _site;

    // initialize so that no collision with an object is detected and that no button is pressed 
    void Start()
    {
        _pressed = false;
        _collided = false;
    }

    // this method is called every frame and checks if the trigger is not pressed anymore or if we select an object 
    void Update()
    {
        checkTriggerUnpressed();

        /* checks if the detector at the index finger collided with a site
         * we need the _collided variable to prevent stepping into the if-loop more than one time (otherwise we would select/deselect the same site every frame)
         * only after we no longer collide with the site we get the possibility to select the site again 
         */
        if (Detector.GetComponent<Detector>().getCollisionState() == true && _collided == false)
        {
            forceFeedback();

            // save the collidedObject (only sites! - for more details see detector script)
            _site = Detector.GetComponent<Detector>().getCollidedObject();

            // checks if the site is already selected (via the site script), if yes we deactivate the site otherwise we active the site by calling the activateSites script
            if (_site.GetComponent<Site>().getSelectionState() == false)
            {
                ActivateSites.setActive(_site);
            }
            else
            {
                ActivateSites.setInactive(_site);
            }
            _collided = true;
        }
        // if our detector no longer collides with the site, we can select another one 
        if (Detector.GetComponent<Detector>().getCollisionState() == false)
        {
            _collided = false;
        }
    }

    //generate a vibritaion sequence out of an audioclip and make the collided controller vibrate 
    private void forceFeedback()
    {
        OVRHapticsClip oVRHapticsClip = new OVRHapticsClip(HapticAudioClip);
        if (this.tag == "leftController")
        {
            OVRHaptics.LeftChannel.Preempt(oVRHapticsClip);
        }
        if (this.tag == "rightController")
        {
            OVRHaptics.RightChannel.Preempt(oVRHapticsClip);
        }
    }

    //checks if trigger is not pressed anymore 
    private void checkTriggerUnpressed()
    {
        if (gameObject.tag == "leftController")
        {
            if (!((OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)) > 0.8) && _pressed)
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
    }

    // if the trigger is not pressed anymore we call the VRManipulation script to stop interactions with the maps 
    private void ControllerUnpressed()
    {
        _pressed = false;
        VRManip.ControllerUnpressed(gameObject.tag);
    }

    /* this method is called when colliding with the map (https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerStay.html)
     * if trigger is pressed we call the VRManipulation script to allow interactions with the maps 
     * we check the boundary of 0.8 to detect when the trigger is actually pressed 
     * because it already detects changes to the trigger as soon as you place your finger on it
     */
    private void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "leftController")
        {
            if ((OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)) > 0.8 && !_pressed)
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