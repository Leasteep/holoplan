using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGUI : MonoBehaviour {

    public Material noDisplay;
    public Material future;
    public Material past;
    public Material futurePast;

    public GameObject futureMap;
    public GameObject pastMap;

    public GameObject gui;

    private Renderer guiRenderer;

	// Use this for initialization
	void Start () {
		
       guiRenderer = gui.GetComponent<Renderer>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
        if(futureMap.activeSelf && pastMap.activeSelf)
        {
            guiRenderer.sharedMaterial = futurePast;
        }
        else if (futureMap.activeSelf)
        {
            guiRenderer.sharedMaterial = future;
        }
        else if (pastMap.activeSelf)
        {
            guiRenderer.sharedMaterial = past;
        }
        else
        {
            guiRenderer.sharedMaterial = noDisplay;
        }
            

    }
}
