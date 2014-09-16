using UnityEngine;
using System.Collections;

public class PlayerSetupController : MonoBehaviour {

	private int playersCount = 0;
	public static Pokemon[] starters;
    public static string[] playerNames;
	private int pokemonChosen = 0;
    public GameObject oakSprite;

    void Start() {
        MusicManager.Instance.playSpeechMusic();
        GUIController.Instance.DisplayBasicDialogs(new string[]{
            "Hello there! Welcome to the world of POKeMON!",
            "My name is OAK! People call me the POKeMON PROF!",
            "This world is inhabited by creatures called POKeMON!",
            "Blah blah blah alcohol. Blah blah blah adventure awaits.",
            "First, how many of you are there?"
        }, () => {
            oakSprite.renderer.enabled = false;
            GUIController.Instance.DisplaySixNumberButtons(selectPlayerNumber);
        }, false);
    }

	void selectPlayerNumber(int numberOfPlayers){
        SFXManager.Instance.playBeep();
		playersCount = numberOfPlayers;
		starters = new Pokemon[numberOfPlayers];
        playerNames = new string[numberOfPlayers];
        StartCoroutine(displayPickers());
	}

    private IEnumerator displayPickers() {
        while (pokemonChosen < playersCount) {
            int playerNumber = pokemonChosen + 1;
            PlayerNameInputManager.Instance.Show("Player " + playerNumber);
            yield return StartCoroutine(GUIController.Instance.DisplayDialogThenStarterPicker("Player " + playerNumber + ", choose your POKeMON!", (Pokemon starter) =>
            {
                starters[pokemonChosen] = starter;
                playerNames[pokemonChosen] = PlayerNameInputManager.Instance.Finsihed();
                pokemonChosen++;
                
            }));
        }

        oakSprite.renderer.enabled = true;
        GUIController.Instance.DisplayBasicDialogs(new string[]{
            "Blah blah adventure."
        }, () =>
        {
            Application.LoadLevel(2);
        });

    }
	
	void startGame(){
		
	}

}
