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
    // auf map anchor ziehen, weil nicht map skaliert wird, sondern anchor
    // dadurch wird indirekt die Map skaliert, da sie Child von anchor ist
	// Use this for initialization
	void Start () {
        //map = this.GetComponentInChildren<GameObject>();

        rendererPresentMap = PresentMap.GetComponent<Renderer>();
        rendererPresentMap.enabled = true;
        rendererPresentMap.sharedMaterial = map_largeScale;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.localScale.x < 2.5)
        {
            rendererPresentMap.sharedMaterial = map_largeScale;
        }

        else if (transform.localScale.x < 5.5)
        {
            rendererPresentMap.sharedMaterial = map_middleScale;
        }

        else {
            rendererPresentMap.sharedMaterial = map_smallScale;
        }


    }
}
