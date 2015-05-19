using UnityEngine;
using System.Collections;

public class PhysicsBlock : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	Ray ray = new Ray(transform.position,Vector3.down);
	
	if(!Physics.Raycast(ray,0.6f) && GameObject.Find ("Character").GetComponent<PlayerMovement>().isPickingUp == false){
		transform.Translate(0,-.5f,0);
	}
	
	
	}
}
