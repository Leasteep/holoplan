using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineChildren : MonoBehaviour {

    public GameObject PresentMap;
    public GameObject FutureMap;
    public GameObject PastMap;

    private Renderer FutureRender;
    private Renderer PresentRender;
    private Renderer PastRender;

    // Use this for initialization
    void Start () {
        PresentRender = PresentMap.GetComponent<Renderer>();
       // PresentRender.enabled = false;
        FutureRender = FutureMap.GetComponent<Renderer>();
        FutureRender.enabled = false;
        PastRender = PastMap.GetComponent<Renderer>();
        PastRender.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void setChildren(GameObject MapParent)
    {
        if (MapParent.tag == "presentMap")
        {
           
            FutureMap.transform.parent = MapParent.transform;
            PastMap.transform.parent = MapParent.transform;
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

    // 3 Szenarien, 1) Verbindung in Zukunft, 2) Verbindung in Vergangenheit, 3) Verbindung in Zukunft und Vergangenheit 
   public void setConnection()
    {
        FutureRender.enabled = true;
        PastRender.enabled = true;
    }
    public void deletConnection()
    { 
            FutureRender.enabled = false;
            PastRender.enabled = false;       
    }
}
