using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Site : MonoBehaviour {

    public string connectedSitesTag;

    public Renderer renderer;
    public Material notSelected;
    public Material selected;

    private int count = 0;
    private bool _selected;
    private GameObject [] connectedSites;

	// Use this for initialization
	void Start () {
        //if abfrage ob null? 
        renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = notSelected;

        connectedSites = GameObject.FindGameObjectsWithTag(connectedSitesTag); //speichert alle verbundenen Sites in einer Liste
        _selected = false;
	}

    public void selectSite()
    {

        _selected = true;
        renderer.sharedMaterial = selected;
       // selectConnectedSites();
    }
    public void disselectSite()
    {
        _selected = false;
        renderer.sharedMaterial = notSelected;
    }
       

    private void selectConnectedSites()
    {
        foreach (GameObject s in connectedSites)
        {
            if (s.activeSelf == false) //wenn die Map auf der die Site ist noch nicht angezeigt wird dann sichtbar machen und Site aktivieren 
            {
                s.transform.parent.gameObject.SetActive(true);
            }
            s.GetComponent<Site>().selectSite();

            //ToDo: Verbindungen starten
        }
    }

    private void disselectConnectedSites()
    {
        foreach (GameObject s in connectedSites)
        {
            s.transform.parent.gameObject.SetActive(false);
            s.GetComponent<Site>().disselectSite();
        }
    }
}

