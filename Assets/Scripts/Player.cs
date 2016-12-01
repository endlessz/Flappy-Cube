using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

	[Header("Movement of the player")]
	public float jumpHeight;
	public float forwardSpeed;

	[Header("Sound Effect")]
	public AudioClip deadSound; //Dead sound
	public AudioClip moveSound; //Jump sound
	public AudioClip scoreSound; //Score sound

	private Rigidbody2D mainRigidbody2D;

	void Start()
	{
		mainRigidbody2D = GetComponent<Rigidbody2D> ();
		mainRigidbody2D.isKinematic = true; //Player not fall when in PREGAME states
	}
	
	// Update is called once per frame
	void Update () {
		//If Player go out off screen
		if ((transform.position.y > getMaxWidth() || transform.position.y < -getMaxWidth() ) && GameManager.instance.currentState == GameStates.INGAME) {
			Dead();
		}
			
		//When click or touch and in INGAME states
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && GameManager.instance.currentState == GameStates.INGAME){
			Jump();
		}

		//When click or touch and in PREGAME states
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && GameManager.instance.currentState == GameStates.PREGAME){
			mainRigidbody2D.isKinematic = false;
			GameManager.instance.startGame(); 
		}
	}
	
	private void Jump(){
		SoundManager.instance.PlaySingleSoundEffect(moveSound);
		mainRigidbody2D.velocity = new Vector2(forwardSpeed,jumpHeight);
	}

	private void Dead(){
		mainRigidbody2D.freezeRotation = false;
		SoundManager.instance.PlaySoundEffect(deadSound);
		GameManager.instance.gameOver ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Obstacle" && GameManager.instance.currentState == GameStates.INGAME) {
			Dead ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Score" && GameManager.instance.currentState == GameStates.INGAME) {
			SoundManager.instance.PlaySoundEffect(scoreSound);
			GameManager.instance.addScore();
			ObstacleSpawner.instance.spawnObstacle();

			Destroy(other.gameObject);
		}
	}

	private float getMaxWidth(){
		Vector2 cameraWidth = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height)); 
		float playerWidth = GetComponent<Renderer>().bounds.extents.y;

		return cameraWidth.y + playerWidth;
	}
}
