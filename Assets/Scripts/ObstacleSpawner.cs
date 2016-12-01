using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {
	public static ObstacleSpawner instance;

	[Header("Obstacle")]
	public GameObject obstacle;

	[Header("Spawn Position")]
	[Tooltip("For random y position of obstacle")]
	public float rangeYPosition;
	[Tooltip("Distance between obstacle")]
	public float distanceXPosition;

	private float lastedXPosition;

	void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		initialObstacles (2);
	}

	public void initialObstacles(int amount){
		for(int i = 0 ; i < amount ; i++){
			spawnObstacle();
		}
	}
	
	public void spawnObstacle(){
		float yPosition = Random.Range(-rangeYPosition, rangeYPosition);
		float xPosition = lastedXPosition += distanceXPosition;

		Vector3 spawnPosition = new Vector3 (xPosition, yPosition, 0.0f);

		Instantiate (obstacle, spawnPosition , Quaternion.identity); //Create obstacle to game
	}
}
