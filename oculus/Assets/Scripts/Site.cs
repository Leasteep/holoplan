using UnityEngine;

/* Each site has an instance of this script
 * This script is used to manage the color change when the site is selected/deselected,
 * and holds an array of sites connected to the selected site.
 * Sites are connected if they have any dependencies in our plannig scenario 
 */
public class Site : MonoBehaviour
{
    //Array with all connected sites 
    public GameObject[] ConnectedSites;
    public Material NotSelected;
    public Material Selected;

    private bool _selected;

    void Start()
    {
        _selected = false;
    }

    // this method is called from the ActivateSites script to activate the site by making it visibile and changing the material
    public void selectSite()
    {
        _selected = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().material = Selected;
    }

    /* This method is called from the ActivateSites script to inactivate the site by making it invisibile and changing the material 
     * sites on the middle map (= presentMap) never become invisible
     */
    public void deselectSite()
    {
        _selected = false;
        gameObject.GetComponent<MeshRenderer>().material = NotSelected;

        if (!this.transform.parent.gameObject.tag.Equals("presentMap"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // This method is called from the VRManipulative script when the controller collides with an site
    public bool getSelectionState()
    {
        return _selected;
    }

    // This method is called from the ActivateSites script to allow manipulation for all connected sites synchronously
    public GameObject[] getConnectedSites()
    {
        return ConnectedSites;
    }

}

