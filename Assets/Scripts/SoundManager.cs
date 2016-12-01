using UnityEngine;
using System.Collections;

[System.Serializable]
public class SoundConstants{
	public const string ON = "on";
	public const string OFF = "off";
}

public class SoundManager : MonoBehaviour {

	public AudioSource musicSource;
	public AudioSource efxSource; 
	public static SoundManager instance = null;
	
	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	void Update(){
		updatingMusicStatus();
		updatingSoundEffectStatus();
	}

	private void updatingMusicStatus(){
		if (PlayerPrefs.GetString ("music" , SoundConstants.ON ) == SoundConstants.OFF) {
			musicSource.mute = true;
		} else {
			musicSource.mute = false;
		}
	}

	private void updatingSoundEffectStatus(){
		if (PlayerPrefs.GetString ("soundEffect", SoundConstants.ON) == SoundConstants.OFF) {
			efxSource.mute = true;
		} else { 
			efxSource.mute = false;
		}
	}
	
	public void PlaySingleSoundEffect(AudioClip clip)
	{
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void PlaySoundEffect(AudioClip clip)
	{
		if (!efxSource.mute) {
			GameObject temporarySfxObject = new GameObject ("TempAudio");
			AudioSource audioSource = temporarySfxObject.AddComponent<AudioSource> () as AudioSource; 

			audioSource.clip = clip; 
			audioSource.Play (); 

			Destroy (temporarySfxObject, clip.length);
		}
	}

	public void MuteMusic(){
		if (PlayerPrefs.GetString ("music") == SoundConstants.OFF) {
			PlayerPrefs.SetString("music", SoundConstants.ON);
		} else {
			PlayerPrefs.SetString("music",SoundConstants.OFF);
		}
	}

	public void MuteSoundEffect(){
		if (PlayerPrefs.GetString ("soundEffect") == SoundConstants.OFF) {
			PlayerPrefs.SetString("soundEffect", SoundConstants.ON);
		} else {
			PlayerPrefs.SetString("soundEffect",SoundConstants.OFF);
		}
	}

}
