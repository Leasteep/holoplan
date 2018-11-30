using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Site : MonoBehaviour {

    public string connectedSitesTag;


    public Material notSelected;
    public Material selected;

    private Renderer renderer;
    private int count = 0;
    private bool _selected;
    private GameObject[] connectedSites;
  

    // Use this for initialization
    void Start () { 
        renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = notSelected;
     
        connectedSites = GameObject.FindGameObjectsWithTag(connectedSitesTag); //speichert alle verbundenen Sites in einer Liste  //if abfrage ob null?
        _selected = false;

    }

    public void selectAllConnectedSites()
    {
        selectSite();
        selectConnectedSites();
       
    }

    private void selectSite()
    {
        _selected = true;
        renderer.sharedMaterial = selected;
       
    }

    private void selectConnectedSites()
    {
        Debug.Log(connectedSites.Length);
        foreach (GameObject s in connectedSites)
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>();

            if (s.transform.parent.gameObject.GetComponent<Renderer>().isVisible == false) //wenn die Map auf der die Site ist noch nicht angezeigt wird dann sichtbar machen und Site aktivieren 
            {
                s.transform.parent.gameObject.GetComponent<Renderer>().enabled = true;

                /*foreach (Transform child in allChildren)
                {
                    child.gameObject.SetActive(true);
                }
                */
            }
            s.GetComponent<Site>().selectSite();
           
            //ToDo: Verbindungen starten
        }
    }

    public void deselectAllConnectedSites()
    {
        deselectSite();
        deselectConnectedSites();
    }

    private void deselectSite()
    {
        _selected = false;
        renderer.sharedMaterial = notSelected;
        
    }

    private void deselectConnectedSites()
    {
        foreach (GameObject s in connectedSites)
        {
            s.GetComponent<Site>().deselectSite();

            GameObject parentMap = s.transform.parent.gameObject;
            Transform[] allChildren = GetComponentsInChildren<Transform>();

            int activeSites = 0;

            foreach (Transform child in allChildren)
            {

                //wenn keine andere Baustelle auf der Map noch selected ist und es nicht die mittle Map ist, dann ausblenden 
                if (child.GetComponent<Site>().getSelectionState() == true)
                {
                    activeSites++;
                }

            }
            if (activeSites == 0 && !parentMap.CompareTag("presentMap"))
            {
                s.transform.parent.gameObject.GetComponent<Renderer>().enabled = false;
               /*foreach (Transform child in allChildren)
                {
                    child.gameObject.SetActive(false);
                }
                */
            } 
            
        }
    }

    public bool getSelectionState()
    {
        return _selected;
    }
   
}

