using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineChildren : MonoBehaviour {

    public GameObject PresentMap;
    public GameObject FutureMap;
    public GameObject PastMap;
    public GameObject MapAnchor;


    //wird in Site ermittelt --> alle Szenarien da abspielen 


    // Use this for initialization
    void Start () {
       
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void setChildren(GameObject MapParent)
    {
        if (MapParent.tag == "presentMap")
        {
           
            FutureMap.transform.parent = PresentMap.transform;
            PastMap.transform.parent = PresentMap.transform;
        }
        if (MapParent.tag == "futureMap")
        {
           
            PresentMap.transform.parent = MapParent.transform;
            PastMap.transform.parent = MapParent.transform;
        }
        if (MapParent.tag == "pastMap")
        {
            
            PresentMap.transform.parent = MapParent.transform;
            FutureMap.transform.parent = MapParent.transform;
        }
    }

    public void deleteParent()
    {
        FutureMap.transform.parent = MapAnchor.transform;
        PresentMap.transform.parent = MapAnchor.transform;
        PastMap.transform.parent = MapAnchor.transform;

    }

    // 3 Szenarien, 1) Verbindung in Zukunft, 2) Verbindung in Vergangenheit, 3) Verbindung in Zukunft und Vergangenheit 
   public void setConnection()
    {
        FutureMap.SetActive(true);
        PastMap.SetActive(true);
    }
    public void deletConnection()
    {
        FutureMap.SetActive(false);
        PastMap.SetActive(false);
    }
}
