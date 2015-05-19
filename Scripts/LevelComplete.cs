using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour {
	
	private PlayerMovement player;

	// Use this for initialization
	void Awake () {
		player = GameObject.Find ("Character").GetComponent<PlayerMovement> ();

		gameObject.AddComponent<ParticleSystem> ();

		ParticleSystem ps = GetComponent<ParticleSystem> ();
		ps.startLifetime = .85f;
		ps.startSpeed = 1;
		ps.startSize = 0.35f;
		ps.gravityModifier = -.5f;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		RaycastHit hit;
		Ray lookUp = new Ray(transform.position, Vector3.up);
		
		if(Physics.Raycast(lookUp, out hit, 0.5f)){
		
		if(hit.transform.tag == "Player"){
			player.hasWon = true;
		}
		
		}
	
	}
}
