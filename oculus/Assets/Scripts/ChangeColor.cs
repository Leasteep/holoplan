using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Renderer renderer;
    public Material notSelected;
    public Material selected;

    private int count = 0;



    void Start()
    {
       renderer = GetComponent<Renderer>();
       renderer.enabled = true;
        renderer.sharedMaterial = notSelected;
    }


    public void changeColor()
    {
        Material selectedMat = notSelected;
        count += 1;

        if (count % 2 == 0)
        {
            selectedMat = notSelected;
        }

        else if (count % 2 == 1)
        {
            selectedMat = selected;
        }
        /*

        else if (count % 4 == 2)  {
            selectedMat = red;
            }

        else if (count % 4 == 3)  {
            selectedMat = green;
            }
            */
        renderer.sharedMaterial = selectedMat;
    }

}

