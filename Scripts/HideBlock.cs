using UnityEngine;
using System.Collections;

public class HideBlock : MonoBehaviour {

	public bool isActive;
	public Transform Target;
	public bool oneShot;
	public bool canUsePlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	RaycastHit hit;
	Ray lookUp = new Ray(transform.position, Vector3.up);
	
	//Debug.DrawRay (transform.position,Vector3.up);
	
	if(Physics.Raycast(lookUp, out hit, 0.5f)){
	
		if(hit.transform.tag == "Player" && canUsePlayer|| hit.transform.tag == "Interactable" || isActive == true){
			isActive = true;
			Target.GetComponent<MeshRenderer>().enabled = false;
			Target.GetComponent<BoxCollider>().enabled = false;
			this.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
		}	
			
		}
		else{
			if(!oneShot){
				isActive = false;
				Target.GetComponent<MeshRenderer>().enabled = true;
				Target.GetComponent<BoxCollider>().enabled = true;
				this.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
			}
			if(oneShot){
				if(isActive){
					Target.GetComponent<MeshRenderer>().enabled = false;
					Target.GetComponent<BoxCollider>().enabled = false;
				}
				else{
					isActive = false;
					Target.GetComponent<MeshRenderer>().enabled = true;
					Target.GetComponent<BoxCollider>().enabled = true;
					this.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
				}
		}
	
	}
	
}
}