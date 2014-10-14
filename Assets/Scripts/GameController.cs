using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {
	

    //Debug variables
    public bool realMode;
    public int startingTileNumber;
    //Debug variables




    public TileController[] squares;
	public PlayerController[] players;
	
	public int currentPlayerNumber;
	public int currentRoll;
	
	public GameObject playerPrefab;
	
	// Use this for initialization
	void Start () {
		Pokemon[] starters = PlayerSetupController.starters;
        string[] playerNames = PlayerSetupController.playerNames;
        int count = 0;
        foreach (TileController tile in squares)
        {
            tile.setup(this, count);
            count++;
        }
        if (starters == null || starters.Length == 0) {
            starters = new Pokemon[] { Pokemon.charmander, Pokemon.charmander, Pokemon.charmander, Pokemon.charmander, Pokemon.charmander };
            playerNames = new string[] { "Player X", "Player X", "Player X", "Player X", "Player X" };
        }
        setupPlayers(starters, playerNames);
        startGame();
	}

    void setupPlayers(Pokemon[] starters, string[] playerNames)
    {
        players = new PlayerController[starters.Length];
        for (int i = 0; i < starters.Length; i++)
        {
            GameObject playerObj = (GameObject)Instantiate(playerPrefab);
            PlayerController player = playerObj.GetComponent<PlayerController>();
            player.setup(this, i + 1, starters[i], playerNames[i]);
            players[i] = player;
        }
    }

	void startGame(){
		currentPlayerNumber = 1;
		startTurn ();
	}
	
	void startTurn(){
		CameraController.Instance.FocusOnPlayer(getCurrentPlayer());
        getCurrentPlayer().getCurrentTile().PlayMyMusic();
		getCurrentPlayer().takeTurn(nextPlayer);
	}
	
	public PlayerController getCurrentPlayer(){
		return players[currentPlayerNumber-1];
	}
	
	void nextPlayer(){
		currentPlayerNumber ++;
		if(currentPlayerNumber > players.Length){
			currentPlayerNumber = 1;
		}
		startTurn();
	}
	
	public TileController getRandomTile(){
		int result = Random.Range(0, squares.Length-1);
		return squares[result];
		
	}

    public void endGame() {
        GUIController.Instance.DisplayDialog(getCurrentPlayer().getName() + " wins!", () => {
            Application.LoadLevel(0);
        });
    }

    public int lastTileNum() {
        return this.squares.Length - 1;
    }

    public PlayerController[] GetPlayersOnTile(int tileNum) { 
        List<PlayerController> playersOnTile = new List<PlayerController>();
        foreach(PlayerController player in players){
            if (player.currentTileNumber == tileNum) {
                playersOnTile.Add(player);
            }
        }

        return playersOnTile.ToArray();
    }

    public TileController GetTileByNum(int tileNum) {
        return this.squares[tileNum];
    }

    public PlayerController getPreviousPlayer() {
        int previousPlayerIndex = currentPlayerNumber - 2;
        if (previousPlayerIndex < 0) {
            previousPlayerIndex = players.Length - 1;
        }
        return players[previousPlayerIndex];
    }

    public int GetNumberOfPlayers() {
        return this.players.Length;
    }
	
}

