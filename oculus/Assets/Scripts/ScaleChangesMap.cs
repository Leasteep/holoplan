using System.Collections;
using UnityEngine;

/* This script is used to switch the material of the maps depending on the scale factor (of the mapAnchor)
 * Therefore we use three map textures of the same part of Hamburg, but with different detail 
 */
public class ScaleChangesMap : MonoBehaviour
{
    public Material Map_smallScale;
    public Material Map_middleScale;
    public Material Map_largeScale;
    public GameObject PresentMap;
    public GameObject FutureMap;
    public GameObject PastMap;

    private Renderer _rendererPresentMap;
    private Renderer _rendererFutureMap;
    private Renderer _rendererPastMap;
    private ArrayList _mapsRenderer;


    //initialise an array list with all renderers of the three maps and assign the large scale material to all
    void Start()
    {
        _rendererPresentMap = PresentMap.GetComponent<Renderer>();
        _rendererFutureMap = FutureMap.GetComponent<Renderer>();
        _rendererPastMap = PastMap.GetComponent<Renderer>();
        _mapsRenderer = new ArrayList();
        _mapsRenderer.Add(_rendererPresentMap);
        _mapsRenderer.Add(_rendererFutureMap);
        _mapsRenderer.Add(_rendererPastMap);

        foreach (Renderer mapRenderer in _mapsRenderer)
        {
            mapRenderer.sharedMaterial = Map_largeScale;
        }
    }

    /* checks the scale of the mapAnchor every frame
     * (not of the maps because we only change the scale values of the anchor)
     * The maps also scale because they are children of the anchor, but they don't change their scale values themselves
     * depending on the scale factor we assign another material to the maps 
     * Currently only the large and small scale are used, but it is also possible to use the middle scale again in future iterations
     */
    void Update()
    {
        if (transform.localScale.x < 1.5)
        {
            foreach (Renderer mapRenderer in _mapsRenderer)
            {
                mapRenderer.sharedMaterial = Map_largeScale;
            }
        }
           /*
            else if (transform.localScale.x < 2.5) {
                foreach (Renderer mapRenderer in mapsRenderer) {
                    mapRenderer.sharedMaterial = map_middleScale;
                }
            }
            */

        else
        {
            foreach (Renderer mapRenderer in _mapsRenderer)
            {
                mapRenderer.sharedMaterial = Map_smallScale;
            }
        }
    }
}
