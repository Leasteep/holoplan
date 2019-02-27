using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/* This is the main script to manipulate the connected sites and activate the connections 
 * This script is called by the VRManipulative script when the controller activates/deactivates a site 
 */
public class ActivateSites : MonoBehaviour
{
    private List<GameObject> _siteList;
    private GameObject _lastSelectedSite;
    private GameObject _connection;

    void Start()
    {
        _lastSelectedSite = null;
    }

    /* puts the collided site and all connected sites in one list to manipulate all synchronously
     * because we only allow to select one site at the time, we inactivate the last selected site before we active the new one
     */
    public void setActive(GameObject site)
    {
        //deactivate the last selected site 
        if (_lastSelectedSite != null && !site.Equals(_lastSelectedSite))
        {
            setInactive(_lastSelectedSite);
        }
        // put all sites we want to maipulate in one List
        GameObject[] siteArray = site.GetComponent<Site>().getConnectedSites();
        _siteList = siteArray.ToList();
        _siteList.Add(site);

        //iteratates through the list to activate the sites and the top and/or bottom map depending on the connections 
        //(the map in the middle is always active)
        foreach (GameObject s in _siteList)
        {
            //saves the map on which the current site from the list is placed and activates it (if necessary) 
            GameObject parentMap = s.transform.parent.gameObject;
            if (parentMap.activeSelf == false)
            {
                parentMap.SetActive(true);
            }

            //call the site script to change its material and selection state 
            s.GetComponent<Site>().selectSite();
        }

        // if a site has a connection (Object) as a child, we iterate over all the children to activate them too 
        if (site.GetComponent<Transform>().childCount > 0)
        {
            int count = site.GetComponent<Transform>().childCount;
            for (int i = 0; i < count; i++)
            {
                _connection = site.transform.GetChild(i).gameObject;
                //use a coroutine to create a fading effect
                startFadeingIn();
            }
        }

        // set the collided site as last selected site
        _lastSelectedSite = site;
    }

    // same as the setActive method 
    public void setInactive(GameObject site)
    {
        if (_lastSelectedSite != null && !site.Equals(_lastSelectedSite))
        {
            setInactive(_lastSelectedSite);
        }

        GameObject[] siteArray = site.GetComponent<Site>().getConnectedSites();
        _siteList = siteArray.ToList();
        _siteList.Add(site);
        foreach (GameObject s in _siteList)
        {
            GameObject parentMap = s.transform.parent.gameObject;
            if (parentMap.CompareTag("presentMap") == false && parentMap.activeSelf == true)
            {
                parentMap.SetActive(false);
            }

            s.GetComponent<Site>().deselectSite();
        }

        if (site.GetComponent<Transform>().childCount > 0)
        {
            int count = site.GetComponent<Transform>().childCount;

            for (int i = 0; i < count; i++)
            {
                _connection = site.transform.GetChild(i).gameObject;
                startFadeingOut();
            }
        }

        _lastSelectedSite = site;
    }

    // used to scale up the alpha value of the color of the connection object, by using a time buffer 
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

    // used to scale down the alpha value of the color of the connection object, by using a time buffer 
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

    // start a coroutine, used in the setActive method
    public void startFadeingIn()
    {
        StartCoroutine("FadeIn");
    }

    // start a coroutine, used in the setInactive method
    public void startFadeingOut()
    {
        StartCoroutine("FadeOut");
    }
}
