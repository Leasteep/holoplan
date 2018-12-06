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
        gameObject.GetComponent<MeshRenderer>().material = notSelected;
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

