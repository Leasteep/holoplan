using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSites : MonoBehaviour
{

    private List<GameObject> siteList;

    // Use this for initialization
    void Start()
    {

    }


    public void setActive(GameObject site)
    {
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

            ParticleSystem em;
            if (s.GetComponentInChildren<ParticleSystem>() != null)
            {
                int count = s.GetComponent<Transform>().childCount;

                for(int i = 0 ; i < count; i++)
                {
                    em = s.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                    em.Play();
                }
                
            }
            // aktiviere Child ParticleSystem
            Debug.Log(s.GetComponent<Site>().getSelectionState().ToString() + s.name);

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



            if (parentMap.CompareTag("presentMap") == false && parentMap.activeSelf == true)
            {
                parentMap.SetActive(false);

            }


            s.GetComponent<Site>().deselectSite();

            ParticleSystem em;
            if (s.GetComponentInChildren<ParticleSystem>() != null)
            {
                int count = s.GetComponent<Transform>().childCount;

                for (int i = 0; i < count; i++)
                {
                    em = s.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                    em.Clear();
                    em.Stop();
                }

            }

            //Debug.Log(s.GetComponent<Site>().getSelectionState().ToString() + s.name);


        }

    }

}