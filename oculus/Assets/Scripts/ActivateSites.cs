using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSites : MonoBehaviour
{
    private List<GameObject> siteList;
    private GameObject _lastSelectedSite;

    // Use this for initialization
    void Start()
    {
        _lastSelectedSite = null;
    }

    public void setActive(GameObject site)
    {
        if (_lastSelectedSite != null && !site.Equals(_lastSelectedSite))
        {
            setInactive(_lastSelectedSite);
        }

        GameObject[] siteArray = site.GetComponent<Site>().getConnectedSites();
        siteList = siteArray.ToList();
        siteList.Add(site);

        foreach (GameObject s in siteList)
        {
            GameObject parentMap = s.transform.parent.gameObject;

            if (parentMap.activeSelf == false)
            //wenn die Map auf der die Site ist noch nicht angezeigt wird dann sichtbar machen und Site aktivieren 
            {
                parentMap.SetActive(true);

            }

            s.GetComponent<Site>().selectSite();

            GameObject connection;
            if (s.GetComponent<Transform>().childCount > 1)
            {
                int count = s.GetComponent<Transform>().childCount;

                for(int i = 0 ; i < count; i++)
                {
                    connection = s.transform.GetChild(i).gameObject;
                    connection.SetActive(true);
                }
                
            }
            // aktiviere Child ParticleSystem
            Debug.Log(s.GetComponent<Site>().getSelectionState().ToString() + s.name);

            //ToDo: Verbindungen starten

        }
        _lastSelectedSite = site;
    }

    public void setInactive(GameObject site)
    {
        GameObject[] siteArray = site.GetComponent<Site>().getConnectedSites();
        siteList = siteArray.ToList();
        siteList.Add(site);

        foreach (GameObject s in siteList)
        {
            GameObject parentMap = s.transform.parent.gameObject;



            if (parentMap.CompareTag("presentMap") == false && parentMap.activeSelf == true)
            {
                parentMap.SetActive(false);

            }


            s.GetComponent<Site>().deselectSite();

            GameObject connection;
            if (s.GetComponent<Transform>().childCount > 1)
            {
                int count = s.GetComponent<Transform>().childCount;

                for (int i = 0; i < count; i++)
                {
                    connection = s.transform.GetChild(i).gameObject;
                    connection.SetActive(false);
                }

                //Debug.Log(s.GetComponent<Site>().getSelectionState().ToString() + s.name);


            }

        }
    }
}