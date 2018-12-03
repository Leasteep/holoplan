using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Site : MonoBehaviour {

    //Array mit positiv (blau) verbundenen Baustellen
    public GameObject[] positiveConnectedSites;

    public Material notSelected;
    public Material selected;

    public Renderer renderer;
    private bool _selected;

  
    void Start () { 
       // renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = notSelected;
        _selected = false;
    }
    

    //aktiviert die aktuelle sowie alle verbundenen Baustellen
    public void selectAllConnectedSites()
    {
        selectSite();
        selectConnectedSites();
       
    }

    //aktiviert die aktuelle Baustelle 
    private void selectSite()
    {
        _selected = true;
        renderer.sharedMaterial = selected;
       
    }

    //aktiviert die verbundenen Baustellen 
    private void selectConnectedSites()
    {
        //geht alle Elemente des Arrays durch 
        foreach (GameObject s in positiveConnectedSites)
        {
            GameObject parentMap = s.transform.parent.gameObject;

            if (parentMap.GetComponent<Renderer>().isVisible == false) 
                //wenn die Map auf der die Site ist noch nicht angezeigt wird dann sichtbar machen und Site aktivieren 
            {
                parentMap.GetComponent<Renderer>().enabled = true;

                //aktiviert alle Baustellen (Child von der ParentMap) 
                Transform[] allChildren = parentMap.GetComponentsInChildren<Transform>(true);
                foreach (Transform child in allChildren)
                {
                    child.gameObject.SetActive(true);
                }
                
            }

            Debug.Log(s.name);
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
        foreach (GameObject s in positiveConnectedSites)
        {
            //s.GetComponent<Site>().deselectSite();

            GameObject parentMap = s.transform.parent.gameObject;
           
            Transform[] allChildren = parentMap.GetComponentsInChildren<Transform>();
            int activeSites = 0;
            foreach (Transform child in allChildren)
            {
                 Debug.Log(child.name);
                //wenn keine andere Baustelle auf der Map noch selected ist und es nicht die mittle Map ist, dann ausblenden 
                if (child.GetComponent<Site>().getSelectionState() == true)
                {
                    activeSites++;
                }

            }
            if (activeSites == 0 && !parentMap.CompareTag("presentMap"))
            {
                s.transform.parent.gameObject.GetComponent<Renderer>().enabled = false;
               foreach (Transform child in allChildren)
                {
                    child.gameObject.SetActive(false);
                }
                
            } 
            
        }
    }

    public bool getSelectionState()
    {
        return _selected;
    }

}

