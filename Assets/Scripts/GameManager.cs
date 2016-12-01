using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameStates{
	PREGAME, 	//State before play
	INGAME, 	//State when play and player alive
	GAMEOVER ,	//State when player dead
}
	
public class GameManager : MonoBehaviour {
	[Header("Text")]
	public Text scoreText;
	public Text newBestScoreText; 
	public Text gameoverScoreText; 
	public Text gameoverBestScoreText; 
	public Text gamesPlayedText; 

	[Header("Canvas UI")]
	public Canvas pregameCanvas; 
	public Canvas gameoverCanvas; 
	public Canvas ingameCanvas;
	public Canvas pauseCanvas; 

	[HideInInspector]
	public static GameManager instance;
	public GameStates currentState;
	private int score;

	void Awake () {
		instance = this;
		currentState = GameStates.PREGAME; //Begin with PREGAME state
	}

	public void addScore(){
		score++;
		scoreText.text = score.ToString();
	}

	public void startGame(){
		currentState = GameStates.INGAME; //Change state to INGAME
		pregameCanvas.gameObject.SetActive (false); //Hide pregameCanvas
		ingameCanvas.gameObject.SetActive (true); //Show ingameCanvas
		PlayerPrefs.SetInt("gamesPlayed", PlayerPrefs.GetInt ("gamesPlayed", 0) + 1 );
	}
	
	public void gameOver(){
		currentState = GameStates.GAMEOVER;

		checkHighScore ();
		gamesPlayedText.text += PlayerPrefs.GetInt ("gamesPlayed", 0);
		gameoverScoreText.text = score.ToString();
		gameoverBestScoreText.text += PlayerPrefs.GetInt ("bestscore", 0);

		StartCoroutine (ShowGameoverCanvas ());
	}

	public void restartGame(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	IEnumerator ShowGameoverCanvas(){
		yield return new WaitForSeconds (1.5f);

		gameoverCanvas.gameObject.SetActive (true);
		ingameCanvas.gameObject.SetActive (false);
	}
	
	private void checkHighScore(){
		if( score > PlayerPrefs.GetInt("bestscore", 0) ) {
			PlayerPrefs.SetInt("bestscore", score);
			newBestScoreText.gameObject.SetActive(true);
		}
	}

	public void Pause(){
		Time.timeScale = 0; //Change timeScale to 0
		pauseCanvas.gameObject.SetActive (true); //Show pauseCanvas
	}

	public void Resume(){
		Time.timeScale = 1; //Change timeScale to 1
		pauseCanvas.gameObject.SetActive (false); //Hide pauseCanvas
	}
}