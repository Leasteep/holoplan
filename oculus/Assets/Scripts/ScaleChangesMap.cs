using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChangesMap : MonoBehaviour {

    public Material map_smallScale;
    public Material map_middleScale;
    public Material map_largeScale;

    public GameObject PresentMap;
    public GameObject FutureMap;
    public GameObject PastMap;


    private Renderer rendererPresentMap;
    private Renderer rendererFutureMap;
    private Renderer rendererPastMap;



    private ArrayList mapsRenderer;

    // auf map anchor ziehen, weil nicht map skaliert wird, sondern anchor
    // dadurch wird indirekt die Map skaliert, da sie Child von anchor ist
	// Use this for initialization

    //wird immer für alle maps gemacht egal ob sichtbar oder nicht 
	void Start () {

        mapsRenderer = new ArrayList();
        rendererPresentMap = PresentMap.GetComponent<Renderer>();
        rendererFutureMap = FutureMap.GetComponent<Renderer>();
        rendererPastMap = PastMap.GetComponent<Renderer>();


      
        mapsRenderer.Add(rendererPresentMap);
        mapsRenderer.Add(rendererFutureMap);
        mapsRenderer.Add(rendererPastMap);

       
        foreach(Renderer mapRenderer in mapsRenderer)
        {
            mapRenderer.sharedMaterial = map_largeScale;
        }

    }

    // Update is called once per frame
    void Update () {

        if (transform.localScale.x < 1.5)
        {
            foreach (Renderer mapRenderer in mapsRenderer)
            {
                mapRenderer.sharedMaterial = map_largeScale;
            }

        }
/*
        else if (transform.localScale.x < 2.5)
        {
            foreach (Renderer mapRenderer in mapsRenderer)
            {
                mapRenderer.sharedMaterial = map_middleScale;
            }
        }
        */

        else {

            foreach (Renderer mapRenderer in mapsRenderer)
            {
                mapRenderer.sharedMaterial = map_smallScale;
            }
        }


    }
}
