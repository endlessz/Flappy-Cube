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
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))){
			Jump();
		}
	}
	
	void Jump(){
		mainRigidbody2D.velocity = new Vector2(forwardSpeed,jumpHeight);
	}

	void Dead(){
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
}
