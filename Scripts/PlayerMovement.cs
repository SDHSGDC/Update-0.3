using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public int movesLeft;

	[HideInInspector]
	public bool blockToLeft;
	[HideInInspector]
	public bool blockToRight;
	[HideInInspector]
	public bool blockToFront;
	[HideInInspector]
	public bool blockToBack;
	[HideInInspector]
	public bool isPickingUp;

	[HideInInspector]
	public Transform charObject;
	[HideInInspector]
	public Transform equippedObject;
	
	static public bool canPlace;

	[HideInInspector]
	public bool canMoveLeft;
	[HideInInspector]
	public bool canMoveRight;
	[HideInInspector]
	public bool canMoveForward;
	[HideInInspector]
	public bool canMoveBackward;

	[HideInInspector]
	public bool canStartLeftCoroutine;
	[HideInInspector]
	public bool canStartRightCoroutine;
	[HideInInspector]
	public bool canStartForwardCoroutine;
	[HideInInspector]
	public bool canStartBackwardCoroutine;

	[HideInInspector]
	public bool canMoveLoopLeft;
	[HideInInspector]
	public bool canMoveLoopRight;
	[HideInInspector]
	public bool canMoveLoopForward;
	[HideInInspector]
	public bool canMoveLoopBackward;

	public Ray ARay2;
	public Ray SRay2;
	public Ray DRay2;
	public Ray WRay2;

	public float FrameWait = 10;

	[HideInInspector]
	public bool canFrameLoopLeft;
	[HideInInspector]
	public bool canFrameLoopRight;
	[HideInInspector]
	public bool canFrameLoopForward;
	[HideInInspector]
	public bool canFrameLoopBackward;

	[Header("Player Sounds")]
	public AudioClip[] Box;
	public AudioClip[] Player;

	[Header("Player Conditions")]
	public bool isDead;
	public bool hasWon;
	private CameraFollow cameraFollow;
	private AudioSource audio;

	void Awake(){
		cameraFollow = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
		charObject = GameObject.Find ("Character").transform;
		canStartLeftCoroutine = true;
		isDead = false;
		audio = this.GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (movesLeft <= 0) {
			isDead = true;	
			cameraFollow.enabled = false;
		}


		if(transform.position.y < -10){
//		Application.LoadLevel(Application.loadedLevel);
			isDead = true;
			cameraFollow.enabled = false;
		}
		
		//Primary Sensors
		Ray ARay = new Ray(GameObject.Find("RayOrigin").transform.position,Vector3.forward);
		Ray SRay = new Ray(GameObject.Find("RayOrigin").transform.position,Vector3.left);
		Ray DRay = new Ray(GameObject.Find("RayOrigin").transform.position,Vector3.back);
		Ray WRay = new Ray(GameObject.Find("RayOrigin").transform.position,Vector3.right);
		
		//Secondary Sensors
		ARay2 = new Ray(GameObject.Find("HeightLimit").transform.position,Vector3.forward);
		SRay2 = new Ray(GameObject.Find("HeightLimit").transform.position,Vector3.left);
		DRay2 = new Ray(GameObject.Find("HeightLimit").transform.position,Vector3.back);
		WRay2 = new Ray(GameObject.Find("HeightLimit").transform.position,Vector3.right);
		
		//Interactable Sensor
		Ray IRay = new Ray(GameObject.Find ("HeightLimit").transform.position, transform.forward);
		
		//Ground Sensor
		Ray GroundRay = new Ray(GameObject.Find ("RayOrigin").transform.position,Vector3.down);

		Debug.DrawRay (GameObject.Find ("RayOrigin").transform.position, Vector3.down);
		Debug.DrawRay (GameObject.Find ("RayOrigin").transform.position, Vector3.forward);
		Debug.DrawRay (GameObject.Find ("RayOrigin").transform.position, Vector3.left);
		Debug.DrawRay (GameObject.Find ("RayOrigin").transform.position, Vector3.right);
		Debug.DrawRay (GameObject.Find ("RayOrigin").transform.position, Vector3.back);
		Debug.DrawRay (GameObject.Find ("HeightLimit").transform.position, transform.forward);
		Debug.DrawRay (GameObject.Find ("HeightLimit").transform.position, Vector3.left);


		
		//Debug.DrawRay (GameObject.Find ("HeightLimit").transform.position, transform.forward, Color.green);
		
		RaycastHit hit;
		
		if(Physics.Raycast(IRay,out hit,1.0f)){
			if(hit.transform.tag == "Interactable"){
				if(Input.GetKeyDown (KeyCode.E) && !isPickingUp){
					isPickingUp = true;
					equippedObject = hit.transform;
					audio.clip = Box[0];
					audio.Play();
				}
			}
			if(isPickingUp){
				equippedObject.transform.SetParent(charObject);
				equippedObject.localScale = new Vector3(.25f,.25f,.25f);
				equippedObject.GetComponent<BoxCollider>().enabled = false;
			}
			
		}	
		
		if(Input.GetKeyDown (KeyCode.E) && isPickingUp && canPlace){
			isPickingUp = false;
			equippedObject.transform.parent = null;
			equippedObject.GetComponent<BoxCollider>().enabled = true;
			equippedObject.transform.localScale = new Vector3(1,1,1);
			if(blockToBack || blockToFront || blockToLeft || blockToRight){
				equippedObject.transform.Translate(0,0.5f,0);
			}
			audio.clip = Box[1];
			audio.Play();
			equippedObject = null;
		}
		
		if(!Physics.Raycast(GroundRay,.5f)){
			transform.Translate(0,-.5f,0);
		}
		
			if(Physics.Raycast(ARay,1.0f) && !Physics.Raycast(ARay2,1.0f)){
				blockToLeft = true;
			}
			else{
				blockToLeft = false;
			}
			
			if(Physics.Raycast(SRay,1.0f) && !Physics.Raycast(SRay2,1.0f)){
				blockToBack = true;
			}
			else{
				blockToBack = false;
			}
			
			if(Physics.Raycast(DRay,1.0f) && !Physics.Raycast(DRay2,1.0f)){
				blockToRight = true;
			}
			else{
				blockToRight = false;
			}
			
			if(Physics.Raycast(WRay,1.0f) && !Physics.Raycast(WRay2,1.0f)){
				blockToFront = true;
			}
			else{
				blockToFront = false;
			}

		if (cameraFollow.isPushingLeft == false) {
			canMoveLeft = false;
			canStartLeftCoroutine = true;
			canMoveLoopLeft = false;
		}

		if (cameraFollow.leftKey) {
			transform.rotation = Quaternion.Euler(0,0,0);
			if(!Physics.Raycast(ARay2,1.0f) && canStartLeftCoroutine == true){
				StartCoroutine (MoveLeft());
			}
		}
		if (cameraFollow.backKey) {
			transform.rotation = Quaternion.Euler(0,-90,0);
			if(!Physics.Raycast(SRay2,1.0f)){
				StartCoroutine (MoveBack());
			}
		}
		if (cameraFollow.rightKey) {
			transform.rotation = Quaternion.Euler(0,180,0);
			if(!Physics.Raycast(DRay2,1.0f)){
				StartCoroutine (MoveRight());
			}
		}
		if (cameraFollow.forwardKey) {
			transform.rotation = Quaternion.Euler(0,90,0);
			if(!Physics.Raycast(WRay2,1.0f)){
				StartCoroutine (MoveForward());
			}
		}

	}

	IEnumerator MoveLeft() {
		movesLeft -= 1;
		canStartLeftCoroutine = false;
		transform.rotation = Quaternion.Euler(0,0,0);
		transform.Translate(Vector3.forward);
		if(blockToLeft){
			transform.Translate(0,.5f,0);
		}
		audio.clip = Player[0];
		audio.Play();
		canMoveLeft = true;
		yield return null;

		if (cameraFollow.isPushingLeft == true) {
			canFrameLoopLeft = true;
			for (int i = 0; i < FrameWait; i++) {
				if (canFrameLoopLeft == true && cameraFollow.isPushingLeft == true) {
					yield return null;
				} else {
					canMoveLeft = false;
					canStartLeftCoroutine = true;
					canFrameLoopLeft = false;
				}
			}

			if (cameraFollow.isPushingLeft == true && canMoveLeft == true) {
				while (canMoveLeft == true && cameraFollow.isPushingLeft == true && (!Physics.Raycast(ARay2,1.0f)) && 
				       !isDead) {
					movesLeft -= 1;
					canMoveLeft = false;
					canMoveLoopLeft = true;
					transform.rotation = Quaternion.Euler(0,0,0);
					transform.Translate(Vector3.forward);
					if(blockToLeft){
						transform.Translate(0,.5f,0);
					}
					for (int i = 0; i < FrameWait; i++) {
						if (canFrameLoopLeft == true && cameraFollow.isPushingLeft == true) {
							yield return null;
						} else {
							canMoveLeft = false;
							canStartLeftCoroutine = true;
							canFrameLoopLeft = false;
						}
					}
		
					if (cameraFollow.isPushingLeft == true && canMoveLoopLeft == true) {
						canMoveLeft = true;
					} else {
						canMoveLeft = false;
						canStartLeftCoroutine = true;
					}
				}
			} else {
				canMoveLeft = false;
				canStartLeftCoroutine = true;
			}
		} else {
			canMoveLeft = false;
			canStartLeftCoroutine = true;
		}

	}

	IEnumerator MoveBack() {
		movesLeft -= 1;
		canStartBackwardCoroutine = false;
		transform.rotation = Quaternion.Euler(0,-90,0);
		transform.Translate(Vector3.forward);
		if(blockToBack){
			transform.Translate(0,.5f,0);
		}
		audio.clip = Player[0];
		audio.Play();
		canMoveBackward = true;
		yield return null;
		if (cameraFollow.isPushingBack == true) {
			canFrameLoopBackward = true;
			for (int i = 0; i < FrameWait; i++) {
					if (canFrameLoopBackward == true && cameraFollow.isPushingBack == true) {
						yield return null;
						} else {
							canMoveBackward = false;
							canStartBackwardCoroutine = true;
							canFrameLoopBackward = false;
						}
			}
			if (cameraFollow.isPushingBack == true && canMoveBackward == true) {
				while (canMoveBackward == true && cameraFollow.isPushingBack == true && (!Physics.Raycast(SRay2,1.0f)) && 
				       !isDead) {
					movesLeft -= 1;
					canMoveLeft = false;
					canMoveLoopBackward = true;
					transform.rotation = Quaternion.Euler(0,-90,0);
					transform.Translate(Vector3.forward);
					if(blockToBack){
						transform.Translate(0,.5f,0);
					}
					for (int i = 0; i < FrameWait; i++) {
						if (canFrameLoopBackward == true && cameraFollow.isPushingBack == true) {
							yield return null;
						} else {
							canMoveBackward = false;
							canStartBackwardCoroutine = true;
							canFrameLoopBackward = false;
						}
					}
					
					if (cameraFollow.isPushingBack == true && canMoveLoopBackward == true) {
						canMoveBackward = true;
					} else {
						canMoveBackward = false;
						canStartBackwardCoroutine = true;
					}
				}
			} else {
				canMoveBackward = false;
				canStartBackwardCoroutine = true;
			}
		} else {
			canMoveBackward = false;
			canStartBackwardCoroutine = true;
		}

	}
	

	IEnumerator MoveRight() {
		movesLeft -= 1;
		canStartRightCoroutine = false;
		transform.rotation = Quaternion.Euler(0,180,0);
		transform.Translate(Vector3.forward);
		if(blockToRight){
			transform.Translate(0,.5f,0);
		}
		audio.clip = Player[0];
		audio.Play();
		canMoveRight = true;
		yield return null;

		if (cameraFollow.isPushingRight == true) {
			canFrameLoopRight = true;
			for (int i = 0; i < FrameWait; i++) {
				if (canFrameLoopRight == true && cameraFollow.isPushingRight == true) {
					yield return null;
				} else {
					canMoveRight = false;
					canStartRightCoroutine = true;
					canFrameLoopRight = false;
				}
			}
			
			if (cameraFollow.isPushingRight == true && canMoveRight == true && !isDead) {
				while (canMoveRight == true && cameraFollow.isPushingRight == true && (!Physics.Raycast(DRay2,1.0f)) && 
				       !isDead) {
					movesLeft -= 1;
					canMoveRight = false;
					canMoveLoopRight = true;
					transform.rotation = Quaternion.Euler(0,180,0);
					transform.Translate(Vector3.forward);
					if(blockToRight){
						transform.Translate(0,.5f,0);
					}
					for (int i = 0; i < FrameWait; i++) {
						if (canFrameLoopRight == true && cameraFollow.isPushingRight == true) {
							yield return null;
						} else {
							canMoveRight = false;
							canStartRightCoroutine = true;
							canFrameLoopRight = false;
						}
					}
					
					if (cameraFollow.isPushingRight == true && canMoveLoopRight == true) {
						canMoveRight = true;
					} else {
						canMoveRight = false;
						canStartRightCoroutine = true;
					}
				}
			} else {
				canMoveRight = false;
				canStartRightCoroutine = true;
			}
		} else {
			canMoveRight = false;
			canStartRightCoroutine = true;
		}
	}

	IEnumerator MoveForward() {
		movesLeft -= 1;
		canStartForwardCoroutine = false;
		transform.rotation = Quaternion.Euler(0,90,0);
		transform.Translate(Vector3.forward);
		if(blockToFront){
			transform.Translate(0,.5f,0);
		}
		audio.clip = Player[0];
		audio.Play();
		canMoveForward = true;
		yield return null;
		if (cameraFollow.isPushingForward == true) {
			canFrameLoopForward = true;
			for (int i = 0; i < FrameWait; i++) {
				if (canFrameLoopForward == true && cameraFollow.isPushingForward == true) {
					yield return null;
				} else {
					canMoveForward = false;
					canStartForwardCoroutine = true;
					canFrameLoopForward = false;
				}
			}
			
			if (cameraFollow.isPushingForward == true && canMoveForward == true && !isDead) {
				while (canMoveForward == true && cameraFollow.isPushingForward == true && (!Physics.Raycast(WRay2,1.0f)) && 
				       !isDead) {
					movesLeft -= 1;
					canMoveForward = false;
					canMoveLoopForward = true;
					transform.rotation = Quaternion.Euler(0,90,0);
					transform.Translate(Vector3.forward);
					if(blockToFront){
						transform.Translate(0,.5f,0);
					} 
					for (int i = 0; i < FrameWait; i++) {
						if (canFrameLoopForward == true && cameraFollow.isPushingForward == true) {
							yield return null;
						} else {
							canMoveForward = false;
							canStartForwardCoroutine = true;
							canFrameLoopForward = false;
						}
					}
					
					if (cameraFollow.isPushingForward == true && canMoveLoopForward == true) {
						canMoveForward = true;
					} else {
						canMoveForward = false;
						canStartForwardCoroutine = true;
					}
				}
			} else {
				canMoveForward = false;
				canStartForwardCoroutine = true;
			}
		} else {
			canMoveForward = false;
			canStartForwardCoroutine = true;
		}
	}
}
