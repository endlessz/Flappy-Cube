using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleSound : MonoBehaviour {

	[Header("Music")]
	public Image musicImage; 						
	public Sprite musicOff, musicOn;

	[Header("Sound Effect")]
	public Image soundEffectImage; 		
	public Sprite soundEffectOff, soundEffectOn;

	// Update is called once per frame
	void Update () {
		updatingMusicSprite();
		updatingSoundEffectSprite();
	}

	private void updatingMusicSprite(){
		if (PlayerPrefs.GetString ("music", SoundConstants.ON) == SoundConstants.OFF) {
			musicImage.GetComponent<Image>().sprite = musicOff;
		} else {
			musicImage.GetComponent<Image>().sprite = musicOn;
		}
	}

	private void updatingSoundEffectSprite(){
		if (PlayerPrefs.GetString ("soundEffect", SoundConstants.ON) == SoundConstants.OFF) {
			soundEffectImage.GetComponent<Image>().sprite = soundEffectOff;
		} else {
			soundEffectImage.GetComponent<Image>().sprite = soundEffectOn;
		}
	}
}
