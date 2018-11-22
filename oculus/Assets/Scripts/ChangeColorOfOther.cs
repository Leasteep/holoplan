using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOfOther : MonoBehaviour {
public Renderer renderer_own;
public Renderer renderer_other;
public Material notSelected;
public Material selected;

public Material red;
public Material green;
public GameObject relatedSite;

private int count = 0;
private Material selectedMat_own;
private Material selectedMat_other;

	
	void Start () {
		renderer_own = GetComponent<Renderer> ();
		renderer_other = relatedSite.GetComponent<Renderer> ();
		renderer_own.enabled = true;
		renderer_other.enabled = true;

		selectedMat_own = notSelected;
		renderer_own.sharedMaterial = selectedMat_own;

		selectedMat_other = red;
		renderer_other.sharedMaterial = selectedMat_other;
	}


	void OnMouseDown () {
		
		if (Input.GetMouseButtonDown (0)) {
			count += 1;
		
			if (count % 2 == 0)  {
				selectedMat_own = notSelected;
				selectedMat_other = red;
				}

			else if (count % 2 == 1)  {
				selectedMat_own = selected;
				selectedMat_other = green;
				}

		Debug.Log("Index: " + count);
		renderer_own.sharedMaterial = selectedMat_own;
		renderer_other.sharedMaterial = selectedMat_other;

		}
		
	}
}
