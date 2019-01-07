using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ActivateSites : MonoBehaviour
{
    private List<GameObject> siteList;
    private GameObject _lastSelectedSite;
    private GameObject _connection;

    // Use this for initialization
    void Start()
    {
        _lastSelectedSite = null;
    }

    public void setActive(GameObject site)
    {
        if (_lastSelectedSite != null && !site.Equals(_lastSelectedSite))
        {
            Debug.Log("schleife" + _lastSelectedSite.name);
            setInactive(_lastSelectedSite);
        }

        Debug.Log("Wooo" + site.name);
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

            if (s.GetComponent<Transform>().childCount > 0)
            {
                int count = s.GetComponent<Transform>().childCount;

                for (int i = 0; i < count; i++)
                {

                    _connection = s.transform.GetChild(i).gameObject;
                    
                    startFadeingIn();
                    // von 0 auf 1 faden 

                    /*for (float f = 0.05f; f <= 0.75f; f += 0.05f)
                    {
                        Color c = connection.GetComponent<Renderer>().material.color;
                        c.a = f;
                        connection.GetComponent<Renderer>().material.color = c;
                        Thread.Sleep(50); 
                    }*/

                }

            }
            s.GetComponent<Site>().selectSite();
            // aktiviere Child ParticleSystem
            //Debug.Log(s.GetComponent<Site>().getSelectionState().ToString() + s.name);

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

           
            if (s.GetComponent<Transform>().childCount > 0)
            {
                int count = s.GetComponent<Transform>().childCount;

                for (int i = 0; i < count; i++)
                {
                    //von 1 auf 0 faden
                    _connection = s.transform.GetChild(i).gameObject;

                    startFadeingOut();
                    /*
                    for (float f = 0.05f; f >= -0.05f; f -= 0.05f)
                    {
                        Color c = connection.GetComponent<Renderer>().material.color;
                        c.a = f;
                        connection.GetComponent<Renderer>().material.color = c;
                        Thread.Sleep(50);
                    }
                    */

                    
                    
                }

                //Debug.Log(s.GetComponent<Site>().getSelectionState().ToString() + s.name);


            }

        }
        _lastSelectedSite = site;
    }
    
    IEnumerator FadeIn()
    {
        GameObject connection = _connection;
        connection.SetActive(true);
        for (float f = 0.05f; f <= 0.75f; f += 0.05f)
        {
            Color c = connection.GetComponent<Renderer>().material.color;
            c.a = f;
            connection.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut()
    {
        GameObject connection = _connection;
        for (float f = 0.75f; f >= -0.05f; f -= 0.2f)
        {
            Color c = connection.GetComponent<Renderer>().material.color;
            c.a = f;
            connection.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        connection.SetActive(false);
    }

    public void startFadeingIn()
    {
        StartCoroutine("FadeIn");
    }
    public void startFadeingOut()
    {
        StartCoroutine("FadeOut");
    }
    
}
