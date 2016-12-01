using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		destroyWhenOutOfScreen();
		destroyWhenGameover();
	}

	private void destroyWhenOutOfScreen(){
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

		//When obstacle out of screen
		if (screenPosition.x < -1){
			Destroy(gameObject);
		}
	}

	private void destroyWhenGameover(){
		if (GameManager.instance.currentState == GameStates.GAMEOVER) {
			Destroy (gameObject, 1.5f);
		}
	}
}
