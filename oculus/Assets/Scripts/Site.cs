using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Site : MonoBehaviour {

    //Array mit positiv (blau) verbundenen Baustellen
    public GameObject[] connectedSites;

    public Material notSelected;
    public Material selected;
    public Renderer renderer; //oder über getComponent?

    private bool _selected;

    void Start () { 
       // renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = notSelected;
        _selected = false;
    }

 
    //aktiviert die aktuelle Baustelle 
    public void selectSite()
    {
        _selected = true;
        Debug.Log("selected");
        renderer = GetComponent<Renderer>();
        renderer.sharedMaterial = selected;
    }

    public void deselectSite()
    {
        _selected = false;
        renderer = GetComponent<Renderer>();
        renderer.sharedMaterial = notSelected;  
    }

    public bool getSelectionState()
    {
        return _selected;
    }

    public GameObject[] getConnectedSites()
    {
        return connectedSites;
    }

  
}

