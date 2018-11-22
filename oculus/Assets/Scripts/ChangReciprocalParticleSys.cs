using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangReciprocalParticleSys : MonoBehaviour {

public GameObject beam;

//private Vars
private ParticleSystem beam_particleSys;
private int count = 0;

	void Start () {
		//Partikelsysteme inaktiv
		beam_particleSys = beam.GetComponent<ParticleSystem>();
		beam_particleSys.Stop();
	}

	void OnMouseDown () {
		
		if (Input.GetMouseButtonDown (0)) {
			count += 1;
		
			if (count % 2 == 0)  {
				beam_particleSys.Clear();
				beam_particleSys.Stop();
				}

			else if (count % 2 == 1)  {
				beam_particleSys.Play();
				}
		}
		
	}
}
