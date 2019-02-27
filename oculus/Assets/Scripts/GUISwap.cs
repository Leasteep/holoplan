using UnityEngine;

/* This script is used to fade in the help pages in the application by pressing the thumbstick
 * when pressed the first time the help appears, the second time the page toggles and the third time the help disappears again
 * when the help pages are active the gui (Ebenenuebersicht) is deactivated
 */
public class GUISwap : MonoBehaviour
{
    public GameObject Gui;
    public GameObject Help;
    public Material Page01;
    public Material Page02;

    private bool _helpActive;
    private bool _pressed;
    private int _pageNum;
    private MeshRenderer _renderer;

    // initially the GUI is activated but transparent and the help pages are deactivated
    void Start()
    {
        _helpActive = false;
        Gui.SetActive(true);
        Help.SetActive(false);
        _pageNum = 0;
        _renderer = Help.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // checks if one of the thumbsticks is pressed 
        if ((OVRInput.Get(OVRInput.Button.PrimaryThumbstick)) || (OVRInput.Get(OVRInput.Button.SecondaryThumbstick)))
        {

            // if the help is not active, activate it and show first page (material = _page01)
            if (!_helpActive)
            {
                Gui.SetActive(false);
                Help.SetActive(true);
                _helpActive = true;
                _renderer.material = Page01;
                _pageNum = 1;
                // used to prevent flicker
                System.Threading.Thread.Sleep(500);
            }

            // if the help is active and the thumbstick is pressed then toogle the page (maertail = _page02)
            else
            {
                if (_pageNum == 1)
                {
                    _renderer.material = Page02;
                    _pageNum = 2;
                    // used to prevent flicker
                    System.Threading.Thread.Sleep(500);
                }

                // if we see the second page and the thumbstick is pressed again, the help gets deactivated 
                else if (_pageNum == 2)
                {
                    Gui.SetActive(true);
                    Help.SetActive(false);
                    _helpActive = false;
                    // used to prevent flicker 
                    System.Threading.Thread.Sleep(500);
                }
            }
        }
    }
}

