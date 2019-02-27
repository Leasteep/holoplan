using System.Collections;
using UnityEngine;

/* This script is used to detect collisions with an object  
 * because we can't detect collisions with the hand avatar we used two little boxes in front of each index finger to detect collisions 
 * These boxes are our detectors
 */
public class Detector : MonoBehaviour
{
    private bool _collision;
    private GameObject _collidedObject;

    void Start()
    {
        _collision = false;
    }

    /* every time we collide with an object this method is called (https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerStay.html)
     * we only allow to save the collided object if it has the tag "site", which are only the sites on the middle map!
    */
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "site")
        {
            _collision = true;
            _collidedObject = obj.gameObject;
        }
    }

    /* this method is called when we stop colliding with the site (https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerExit.html)
     * we decide to use a short delay to make the selection less sensitive because otherwise we would collide more often with sites than we want
     */
    private void OnTriggerExit(Collider other)
    {
        Delay();
        _collision = false;
    }

    // return the collision state
    public bool getCollisionState()
    {
        return _collision;
    }

    // return the collided object (only if it is a site from the middle map)
    public GameObject getCollidedObject()
    {
        return _collidedObject;
    }

    // defines delay duration
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
    }

}

