using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(480, 320, true, 0);
        MusicManager.Instance.playTitleMusic();
	}
}
