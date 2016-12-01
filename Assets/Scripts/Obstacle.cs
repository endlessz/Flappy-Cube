using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		destroyWhenOutOfScreen ();
	}

	private void destroyWhenOutOfScreen(){
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

		//When obstacle out of screen
		if (screenPosition.x < -1){
			Destroy(gameObject);
		}
	}
}
