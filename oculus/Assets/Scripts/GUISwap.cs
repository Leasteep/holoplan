using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISwap : MonoBehaviour
{

    public GameObject _gui;
    public GameObject _help;
    public Material _page01;
    public Material _page02;
    public Material _page03;

    private bool _helpActive;
    private bool _pressed;
    private int _pageNum;
    private MeshRenderer renderer;

    void Start()
    {
        _helpActive = false;
        _gui.SetActive(true);
        _help.SetActive(false);
        _pageNum = 3;
        renderer = _help.GetComponent<MeshRenderer>();

    }

    void Update()
    {
        OVRInput.Update();
        // prüfe ob einer der beiden Joysticks gedrückt wird
        if ((OVRInput.Get(OVRInput.Button.PrimaryThumbstick)) || (OVRInput.Get(OVRInput.Button.SecondaryThumbstick)))
        {
            // wenn Hilfe noch nicht aktiv ist, blende ein und wähle Material
            if (!_helpActive)
            {
                _gui.SetActive(false);
                _help.SetActive(true);
                _helpActive = true;
                renderer.material = _page01;
                _pageNum = 1;
                System.Threading.Thread.Sleep(500);

            }

            // wenn Hilfe schon aktiv ist
            else
            {
                if (_pageNum == 1)
                {
                    renderer.material = _page02;
                    _pageNum = 2;
                    System.Threading.Thread.Sleep(500);
                }

                else if (_pageNum == 2)
                {
                    renderer.material = _page03;
                    _pageNum = 3;
                    System.Threading.Thread.Sleep(500);
                }

                // wenn Seite 3 schon erreicht ist, blende Hilfe wieder aus
                else if (_pageNum == 3)
                {
                    _gui.SetActive(true);
                    _help.SetActive(false);
                    _helpActive = false;
                    System.Threading.Thread.Sleep(500);
                }
            }
        }
    }
}

