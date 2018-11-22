using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChangesMap : MonoBehaviour {

    public Material map_smallScale;
    public Material map_middleScale;
    public Material map_largeScale;

    public GameObject map;

    private Renderer renderer_map;

	// Use this for initialization
	void Start () {
        //map = this.GetComponentInChildren<GameObject>();

        renderer_map = map.GetComponent<Renderer>();
        renderer_map.enabled = true;
        renderer_map.sharedMaterial = map_largeScale;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.localScale.x < 2.5)
        {
            renderer_map.sharedMaterial = map_largeScale;
        }

        else if (transform.localScale.x < 5.5)
        {
            renderer_map.sharedMaterial = map_middleScale;
        }

        else {
            renderer_map.sharedMaterial = map_smallScale;
        }


    }
}
