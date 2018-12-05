using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSites : MonoBehaviour {

    private List<GameObject> siteList;

    // Use this for initialization
    void Start() {

    }

   
    public void setActive(GameObject site)
    {
        GameObject[] siteArray = site.GetComponent<Site>().getConnectedSites();
        siteList = siteArray.ToList();
        siteList.Add(site);
       
        foreach(GameObject s in siteList)
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
                    Debug.Log(child.name);
                    child.gameObject.SetActive(true);

                   /* if(child.transform.childCount > 0 && child.CompareTag("presentMap") == false && child.CompareTag("map") == false) 
                    {

                         Debug.Log(GameObject.Find("/ParticleSystem").tag);
                        
                        
                    }
                    */
                    
                }  

            }

            s.GetComponent<Site>().selectSite();
            // aktiviere Child ParticleSystem
            //Debug.Log(s.GetComponent<Site>().getSelectionState().ToString() + s.name);

            //ToDo: Verbindungen starten
        }
    }

    public void setInactive(GameObject site)
    {
        GameObject[] siteArray = site.GetComponent<Site>().getConnectedSites();
        siteList = siteArray.ToList();
        siteList.Add(site);

        foreach (GameObject s in siteList)
        {
            GameObject parentMap = s.transform.parent.gameObject;
            
            
            Transform[] allChildren = parentMap.GetComponentsInChildren<Transform>();
            
            foreach (Transform child in allChildren)
            {
                Debug.Log(child.name);
                if (parentMap.CompareTag("presentMap") == false && parentMap.GetComponent<Renderer>().isVisible == true)
                {
                    s.transform.parent.gameObject.GetComponent<Renderer>().enabled = false;
                    child.gameObject.SetActive(false);
                    
                }
            }

            s.GetComponent<Site>().deselectSite();
            //Debug.Log(s.GetComponent<Site>().getSelectionState().ToString() + s.name);


        }

    }
}
