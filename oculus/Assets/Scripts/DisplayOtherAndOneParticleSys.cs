using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOtherAndOneParticleSys : MonoBehaviour {

//Variaben, die mit Farben belegt werden
public Renderer renderer_own;
private Renderer renderer_other;
//alle Materialien, auf die zugegriffen wird
public Material notSelected;
public Material selected;

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
		//verstecke anderes Objekt
		renderer_other.enabled = false;

		//Materialien werden gesetzt
		//eigenes Material
		selectedMat_own = notSelected;
		renderer_own.sharedMaterial = selectedMat_own;
		//Partikelsysteme inaktiv
		beam_particleSys = beam.GetComponent<ParticleSystem>();
		beam_particleSys.Stop();
	}

	void OnMouseDown () {
		
		if (Input.GetMouseButtonDown (0)) {
			count += 1;
		
			if (count % 2 == 0)  {
				selectedMat_own = notSelected;
				renderer_other.enabled = false;
				beam_particleSys.Clear();
				beam_particleSys.Stop();
				}

			else if (count % 2 == 1)  {
				selectedMat_own = selected;
				selectedMat_other = selected;
				beam_particleSys.Play();
				renderer_other.enabled = true;
				renderer_other.sharedMaterial = selected;
				}

		//Debug.Log("Index: " + count);
		renderer_own.sharedMaterial = selectedMat_own;
		renderer_other.sharedMaterial = selectedMat_other;

		}
		
	}
}
