using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

	[Header("Movement of the player")]
	public float jumpHeight;
	public float forwardSpeed;

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
		mainRigidbody2D.velocity = new Vector2(forwardSpeed,jumpHeight);
	}

	private void Dead(){
		mainRigidbody2D.freezeRotation = false;
		Debug.Log("Game Over");
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Obstacle") {
			Dead ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Score") {
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
