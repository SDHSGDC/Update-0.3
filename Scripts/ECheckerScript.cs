using UnityEngine;
using System.Collections;

public class ECheckerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	RaycastHit hit;
	Ray eCheck = new Ray(GameObject.Find ("EChecker").transform.position,-transform.forward);
	
	if(Physics.Raycast(eCheck,out hit, 1f)){
		PlayerMovement.canPlace = false;
	}
	else{
		PlayerMovement.canPlace = true;
	}
	
	}
}
