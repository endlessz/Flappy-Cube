using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public Transform player;	//Target to follow;
	public float xPositionOffset;

	private float playerXPosition;
	private float cameraXPosition;

	void Start(){
		cameraXPosition = transform.position.x;
	}

	void Update(){
		playerXPosition = player.transform.position.x - xPositionOffset;  //Position of the player with calculate offset

		if (playerXPosition > cameraXPosition) {
			cameraXPosition = playerXPosition;
		}
			
		moveCamera(cameraXPosition);
	}

	void moveCamera(float xPosition){
		transform.position = new Vector3 (xPosition, transform.position.y , transform.position.z); 
	}
}