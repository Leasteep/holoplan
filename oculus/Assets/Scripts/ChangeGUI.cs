using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this script is used to change our GUI (Ebenenuebersicht) depending on the activated maps 
 * this should be a visual help to support the work with the three maps 
 */
public class ChangeGUI : MonoBehaviour
{
    public Material NoDisplay;
    public Material Future;
    public Material Past;
    public Material FuturePast;
    public GameObject FutureMap;
    public GameObject PastMap;
    public GameObject Gui;

    private Renderer _guiRenderer;

    // initialise the renderer of the gui object
    void Start()
    {
        _guiRenderer = Gui.GetComponent<Renderer>();
    }

    /* checks wich maps are active and changes the material, if neither the top nor the bottom map are active the GUI is invisible (noDisplay)
     * by this "invisible" material we have the chance to keep the whole gui always active 
     */
    void Update()
    {
        if (FutureMap.activeSelf && PastMap.activeSelf)
        {
            _guiRenderer.sharedMaterial = FuturePast;
        }
        else if (FutureMap.activeSelf)
        {
            _guiRenderer.sharedMaterial = Future;
        }
        else if (PastMap.activeSelf)
        {
            _guiRenderer.sharedMaterial = Past;
        }
        else
        {
            _guiRenderer.sharedMaterial = NoDisplay;
        }
    }
}
