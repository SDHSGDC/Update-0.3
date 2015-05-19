using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	private Vector3 offset;
	
	private Vector3 offsetX;
	private Vector3 offsetY;
	private Vector3 offsetZ;
	
	public Transform Target;
	
	private Vector3 velocity = Vector3.zero;

	[HideInInspector]
	public float angleX;

	[HideInInspector]
	public float distanceZ = 10f;
	[HideInInspector]
	public float distanceX = 5f; 
	[HideInInspector]
	public float distanceY = 5f;

	[HideInInspector]
	public bool isRoating = false;

	[HideInInspector]
	public float rotationState;

	[HideInInspector]
	public bool forwardKey;
	[HideInInspector]
	public bool leftKey;
	[HideInInspector]
	public bool rightKey;
	[HideInInspector]
	public bool backKey;
	
    [HideInInspector]
	public bool isPushingForward;
	[HideInInspector]
	public bool isPushingLeft;
	[HideInInspector]
	public bool isPushingRight;
	[HideInInspector]
	public bool isPushingBack;

	// Use this for initialization
	void Start () {
		
		Target = GameObject.Find ("Character").transform;
		
		//distanceZ = Target.transform.position.z - transform.position.z;
		offset = transform.position;
		rotationState = 1;
		/*Quaternion rotation = Quaternion.Euler(35f, angleX + 45f, 0f);
        
        transform.rotation = rotation;*/ 
	}
	
	// Update is called once per frame
	void Update () {
		
		//change camera shit
		
		//        Debug.Log (rotationState % 4);
		//
		//        Debug.Log (forwardKey);
		//        Debug.Log (leftKey);
		//        Debug.Log (rightKey);
		//        Debug.Log (backKey);
		
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			StartCoroutine(RotateRight());
			
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			StartCoroutine(RotateLeft());
		}
		
		if (rotationState % 4 == 1) {
			
			if (Input.GetKeyDown (KeyCode.A)) {
				leftKey = true;
			} else {
				leftKey = false;
			}
			
			if (Input.GetKeyDown (KeyCode.S)) {
				backKey = true;
			} else {
				backKey = false;
			}
			
			if (Input.GetKeyDown (KeyCode.D)) {
				rightKey = true;
			} else {
				rightKey = false;
			}
			
			if (Input.GetKeyDown (KeyCode.W)) {
				forwardKey = true;
			} else {
				forwardKey = false;
			}
			
		} else {
			if (rotationState % 4 == 2) {
				if (Input.GetKeyDown (KeyCode.S)) {
					leftKey = true;
				} else {
					leftKey = false;
				}
				
				if (Input.GetKeyDown (KeyCode.D)) {
					backKey = true;
				} else {
					backKey = false;
				}
				
				if (Input.GetKeyDown (KeyCode.W)) {
					rightKey = true;
				} else {
					rightKey = false;
				}
				
				if (Input.GetKeyDown (KeyCode.A)) {
					forwardKey = true;
				} else {
					forwardKey = false;
				}
			} else {
				if (rotationState % 4 == 3) {
					if (Input.GetKeyDown (KeyCode.D)) {
						leftKey = true;
					} else {
						leftKey = false;
					}
					
					if (Input.GetKeyDown (KeyCode.W)) {
						backKey = true;
					} else {
						backKey = false;
					}
					
					if (Input.GetKeyDown (KeyCode.A)) {
						rightKey = true;
					} else {
						rightKey = false;
					}
					
					if (Input.GetKeyDown (KeyCode.S)) {
						forwardKey = true;
					} else {
						forwardKey = false;
					}
				} else {
					if (rotationState % 4 == 0) {
						if (Input.GetKeyDown (KeyCode.W)) {
							leftKey = true;
						} else {
							leftKey = false;
						}
						
						if (Input.GetKeyDown (KeyCode.A)) {
							backKey = true;
						} else {
							backKey = false;
						}
						
						if (Input.GetKeyDown (KeyCode.S)) {
							rightKey = true;
						} else {
							rightKey = false;
						}
						
						if (Input.GetKeyDown (KeyCode.D)) {
							forwardKey = true;
						} else {
							forwardKey = false;
						}
					} else {
						if (rotationState % 4 == -1) {
							if (Input.GetKeyDown (KeyCode.D)) {
								leftKey = true;
							} else {
								leftKey = false;
							}
							
							if (Input.GetKeyDown (KeyCode.W)) {
								backKey = true;
							} else {
								backKey = false;
							}
							
							if (Input.GetKeyDown (KeyCode.A)) {
								rightKey = true;
							} else {
								rightKey = false;
							}
							
							if (Input.GetKeyDown (KeyCode.S)) {
								forwardKey = true;
							} else {
								forwardKey = false;
							}
						} else {
							if (rotationState % 4 == -2) {
								if (Input.GetKeyDown (KeyCode.S)) {
									leftKey = true;
								} else {
									leftKey = false;
								}
								
								if (Input.GetKeyDown (KeyCode.D)) {
									backKey = true;
								} else {
									backKey = false;
								}
								
								if (Input.GetKeyDown (KeyCode.W)) {
									rightKey = true;
								} else {
									rightKey = false;
								}
								
								if (Input.GetKeyDown (KeyCode.A)) {
									forwardKey = true;
								} else {
									forwardKey = false;
								}
							} else {
								if (rotationState % 4 == -3) {
									
									if (Input.GetKeyDown (KeyCode.A)) {
										leftKey = true;
									} else {
										leftKey = false;
									}
									
									if (Input.GetKeyDown (KeyCode.S)) {
										backKey = true;
									} else {
										backKey = false;
									}
									
									if (Input.GetKeyDown (KeyCode.D)) {
										rightKey = true;
									} else {
										rightKey = false;
									}
									
									if (Input.GetKeyDown (KeyCode.W)) {
										forwardKey = true;
									} else {
										forwardKey = false;
									}
									
								}
							}
						}
					} 
				}
			}
		}
		
		
		
		if (rotationState % 4 == 1) {
			
			if (Input.GetKey (KeyCode.A)) {
				isPushingLeft = true;
			} else {
				isPushingLeft = false;
			}
			
			if (Input.GetKey (KeyCode.S)) {
				isPushingBack = true;
			} else {
				isPushingBack = false;
			}
			
			if (Input.GetKey (KeyCode.D)) {
				isPushingRight = true;
			} else {
				isPushingRight = false;
			}
			
			if (Input.GetKey (KeyCode.W)) {
				isPushingForward = true;
			} else {
				isPushingForward = false;
			}
			
		} else {
			if (rotationState % 4 == 2) {
				if (Input.GetKey (KeyCode.S)) {
					isPushingLeft = true;
				} else {
					isPushingLeft = false;
				}
				
				if (Input.GetKey (KeyCode.D)) {
					isPushingBack = true;
				} else {
					isPushingBack = false;
				}
				
				if (Input.GetKey (KeyCode.W)) {
					isPushingRight = true;
				} else {
					isPushingRight = false;
				}
				
				if (Input.GetKey (KeyCode.A)) {
					isPushingForward = true;
				} else {
					isPushingForward = false;
				}
			} else {
				if (rotationState % 4 == 3) {
					if (Input.GetKey (KeyCode.D)) {
						isPushingLeft = true;
					} else {
						isPushingLeft = false;
					}
					
					if (Input.GetKey (KeyCode.W)) {
						isPushingBack = true;
					} else {
						isPushingBack = false;
					}
					
					if (Input.GetKey (KeyCode.A)) {
						isPushingRight = true;
					} else {
						isPushingRight = false;
					}
					
					if (Input.GetKey (KeyCode.S)) {
						isPushingForward = true;
					} else {
						isPushingForward = false;
					}
				} else {
					if (rotationState % 4 == 0) {
						if (Input.GetKey (KeyCode.W)) {
							isPushingLeft = true;
						} else {
							isPushingLeft = false;
						}
						
						if (Input.GetKey (KeyCode.A)) {
							isPushingBack = true;
						} else {
							isPushingBack = false;
						}
						
						if (Input.GetKey (KeyCode.S)) {
							isPushingRight = true;
						} else {
							isPushingRight = false;
						}
						
						if (Input.GetKey (KeyCode.D)) {
							isPushingForward = true;
						} else {
							isPushingForward = false;
						}
					} else {
						if (rotationState % 4 == -1) {
							if (Input.GetKey (KeyCode.D)) {
								isPushingLeft = true;
							} else {
								isPushingLeft = false;
							}
							
							if (Input.GetKey (KeyCode.W)) {
								isPushingBack = true;
							} else {
								isPushingBack = false;
							}
							
							if (Input.GetKey (KeyCode.A)) {
								isPushingRight = true;
							} else {
								isPushingRight = false;
							}
							
							if (Input.GetKey (KeyCode.S)) {
								isPushingForward = true;
							} else {
								isPushingForward = false;
							}
						} else {
							if (rotationState % 4 == -2) {
								if (Input.GetKey (KeyCode.S)) {
									isPushingLeft = true;
								} else {
									isPushingLeft = false;
								}
								
								if (Input.GetKey (KeyCode.D)) {
									isPushingBack = true;
								} else {
									isPushingBack = false;
								}
								
								if (Input.GetKey (KeyCode.W)) {
									isPushingRight = true;
								} else {
									isPushingRight = false;
								}
								
								if (Input.GetKey (KeyCode.A)) {
									isPushingForward = true;
								} else {
									isPushingForward = false;
								}
							} else {
								if (rotationState % 4 == -3) {
									
									if (Input.GetKey (KeyCode.A)) {
										isPushingLeft = true;
									} else {
										isPushingLeft = false;
									}
									
									if (Input.GetKey (KeyCode.S)) {
										isPushingBack = true;
									} else {
										isPushingBack = false;
									}
									
									if (Input.GetKey (KeyCode.D)) {
										isPushingRight = true;
									} else {
										isPushingRight = false;
									}
									
									if (Input.GetKey (KeyCode.W)) {
										isPushingForward = true;
									} else {
										isPushingForward = false;
									}
									
								}
							}
						}
					} 
				}
			}
		}
		
		
		//offset = (offsetZ);
		//transform.position = Vector3.SmoothDamp(transform.position, Target.transform.position + offset, ref velocity, 0.2f,6f);
		//instead of 0.2f and 6f make it proportional to the distance it needs to travel
		
		
		
		//transform.position = Target.position + rotation * new Vector3(0f, 0f, -distanceZ);    
		
		Quaternion rotation = Quaternion.Euler(35f, angleX + 60f, 0f);
		
		//transform.rotation = rotation; 
		
		transform.position = Vector3.SmoothDamp(transform.position,
		                                        Target.transform.position + rotation * new Vector3(0f, 0f, -distanceZ),
		                                        ref velocity,
		                                        0.2f,
		                                        6f);
		
		//transform.rotation = Quaternion.AngleAxis(angleX, Target.transform.position);
		
	}
	IEnumerator WaitForFrames(float frames) {
		for (int i = 0; i < frames; i++) {
			yield return null;
		}
	}
	IEnumerator RotateRight() {
		for (int i = 0; i < 3; i++) {
			angleX -= 30f;
			transform.RotateAround(Target.position, Vector3.up, -30f);
			yield return null;
			
			if (i == 2) {
				rotationState -= 1;
			}
		}
	}
	IEnumerator RotateLeft() {
		for (int i = 0; i < 3; i++) {
			angleX += 30f;
			transform.RotateAround(Target.position, Vector3.up, 30f);
			yield return null;
			
			if (i == 2) {
				rotationState += 1;
			}
		}
	}
	
	IEnumerator RotateFrames(float angle, float frames, int rotationstatechange) {
		for (int i = 0; i < frames; i++) {
			angleX += angle / frames;
			transform.RotateAround (Target.position, Vector3.up, angle);
			yield return null;
			
			if (i == frames - 1){
				rotationState += rotationstatechange;
			}
		}
	}
}