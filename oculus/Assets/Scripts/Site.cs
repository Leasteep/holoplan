using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Site : MonoBehaviour {

    //Array mit positiv (blau) verbundenen Baustellen
    public GameObject[] connectedSites;

    public Material notSelected;
    public Material selected;


    private bool _selected;

    void Start () { 

        _selected = false;
    }

 
    //aktiviert die aktuelle Baustelle 
    public void selectSite()
    {
        _selected = true;
        Debug.Log("selected");
        gameObject.GetComponent<MeshRenderer>().material = selected;
    }

   
    public void deselectSite()
    {
        _selected = false;
        startFadeingOut();
    }
    

    public bool getSelectionState()
    {
        return _selected;
    }

    public GameObject[] getConnectedSites()
    {
        return connectedSites;
    }

    public void startFadeingOut()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        for (float f = 1.0f; f >= -0.05f; f -= 0.1f)
        {
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.GetComponent<MeshRenderer>().material = notSelected;
    }

}

