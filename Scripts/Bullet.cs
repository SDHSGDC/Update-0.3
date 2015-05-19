using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float bulletLife;
	private PlayerMovement player;

	void Awake(){
		player = GameObject.Find ("Character").GetComponent<PlayerMovement> ();
	}

	// Use this for initialization
	void Start () {

		StartCoroutine (KillBlock (bulletLife));

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Player") {
			//Application.LoadLevel(Application.loadedLevel);
			player.isDead = true;
		}
		Destroy (gameObject);
	}

	IEnumerator KillBlock(float seconds){
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);
	}
}
