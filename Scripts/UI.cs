using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public int uiState;
	private int time;
	private bool showUI;
	private PlayerMovement player;
	private Text moveText;
	private Canvas deathCanvas;
	private Canvas levelSelectCanvas;
	private Canvas timerCanvas;
	private Canvas winCanvas;
	private Text winMoveText;
	private Text loseMoveText;
	public GameObject mainCamera;

	void Awake(){
		player = GameObject.Find ("Character").GetComponent<PlayerMovement> ();
		winMoveText = GameObject.Find ("Win Move Display").GetComponent<Text> ();
		mainCamera = GameObject.Find ("Main Camera");
		deathCanvas = GameObject.Find ("Death Canvas").GetComponent<Canvas>();
		levelSelectCanvas = GameObject.Find ("Level Select Canvas").GetComponent<Canvas>();
		timerCanvas = GameObject.Find ("Timer Canvas").GetComponent<Canvas>();
		moveText = GameObject.Find ("Move Text").GetComponent<Text>();
		winCanvas = GameObject.Find("Win Canvas").GetComponent<Canvas>();
		loseMoveText = GameObject.Find ("Move Display").GetComponent<Text> ();
		player.enabled = true;
	}

	// Use this for initialization
	void Start () {

		uiState = 1;

		showUI = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (player.isDead || player.hasWon) {
			showUI = true;
		}

		moveText.text = "Moves left: " + player.movesLeft.ToString();
		loseMoveText.text = "Moves left: " + player.movesLeft.ToString ();
		winMoveText.text = "Moves left: " + player.movesLeft.ToString();

		if (showUI) {

			if(player.isDead){

				player.enabled = false;

				switch (uiState) {

//				case 0:
//				deathCanvas.enabled = false;
//				levelSelectCanvas.enabled = false;
//				Time.timeScale = 1;
//				break;

				case 1:
				deathCanvas.enabled = true;
				levelSelectCanvas.enabled = false;
				timerCanvas.enabled = false;
				winCanvas.enabled = false;
				
				GameObject.Find("Intro Message Canvas").GetComponent<Canvas>().enabled = false;
				break;

				case 2:
				levelSelectCanvas.enabled = true;
				deathCanvas.enabled = false;
				timerCanvas.enabled = false;
					if(Application.loadedLevel == 0){
						GameObject.Find ("m_Main Menu Canvas").GetComponent<Canvas> ().enabled = false;
						GameObject.Find ("m_Level Select Canvas").GetComponent<Canvas> ().enabled = true;
					}
				break;

				case 3:
				GameObject.Find ("m_Main Menu Canvas").GetComponent<Canvas> ().enabled = true;
				GameObject.Find ("m_Level Select Canvas").GetComponent<Canvas> ().enabled = false;
				GameObject.Find ("m_Credits Canvas").GetComponent<Canvas> ().enabled = false;
				levelSelectCanvas.enabled = false;
				break;

				case 4:
				GameObject.Find ("m_Main Menu Canvas").GetComponent<Canvas> ().enabled = false;
				GameObject.Find ("m_Credits Canvas").GetComponent<Canvas> ().enabled = true;
				break;



				}

		}

			if(player.hasWon){

				player.enabled = false;
				timerCanvas.enabled = false;
				winCanvas.enabled = true;
//				mainCamera.gameObject.AddComponent<ParticleSystem>();
//				ParticleSystem ps = GetComponent<ParticleSystem> ();


			}

		} else {
			deathCanvas.enabled = false;
			levelSelectCanvas.enabled = false;
			timerCanvas.enabled = true;
			winCanvas.enabled = false;
			player.enabled = true;	
		}
	
	}
	
	//MENU BUTTONS

	public void LoadLevel(int level){
		Application.LoadLevel (level);
	}

	public void NextLevel(){
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void RestartLevel(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void ShowLevels(){
		uiState = 2;
	}

	public void GoBack(){
		if (Application.loadedLevel == 0) {
			uiState = 3;
		} else {
			uiState -= 1;
		}
	}

	public void makeState(int state){
		uiState = state;
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
