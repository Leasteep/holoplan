using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorAndOneParticleSys : MonoBehaviour {

//Variaben, die mit Farben belegt werden
public Renderer renderer_own;
private Renderer renderer_other;
//alle Materialien, auf die zugegriffen wird
public Material notSelected;
public Material selected;
public Material other;

//andere Site, auf die zugegriffen wird
public GameObject relatedSite;
//Partikelsysteme, auf die zugegriffen wird
public GameObject beam;

//private Vars
private ParticleSystem beam_particleSys;
private int count = 0;
private Material selectedMat_own;
private Material selectedMat_other;

	void Start () {
		//Renderer werden gesetzt und aktiviert
		renderer_own = GetComponent<Renderer> ();
		renderer_other = relatedSite.GetComponent<Renderer> ();
		renderer_own.enabled = true;
		renderer_other.enabled = true;
		//Materialien werden gesetzt
		//eigenes Material
		selectedMat_own = notSelected;
		renderer_own.sharedMaterial = selectedMat_own;
		//andere Materialien
		selectedMat_other = other;
		renderer_other.sharedMaterial = notSelected;
		//Partikelsysteme inaktiv
		beam_particleSys = beam.GetComponent<ParticleSystem>();
		beam_particleSys.Stop();
	}

	void OnMouseDown () {
		
		if (Input.GetMouseButtonDown (0)) {
			count += 1;
		
			if (count % 2 == 0)  {
				selectedMat_own = notSelected;
				selectedMat_other = notSelected;
				beam_particleSys.Clear();
				beam_particleSys.Stop();
				}

			else if (count % 2 == 1)  {
				selectedMat_own = selected;
				selectedMat_other = other;
				beam_particleSys.Play();
				}

		//Debug.Log("Index: " + count);
		renderer_own.sharedMaterial = selectedMat_own;
		renderer_other.sharedMaterial = selectedMat_other;

		}
		
	}
}
