using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {
	

    //Debug variables
    public bool realMode;
    public int startingTileNumber;
    //Debug variables






    
    private static List<int> goldSquares;
	public TileController[] squares;
	public CameraController cameraController;
	public PlayerController[] players;
	
	public int currentPlayerNumber;
	public int currentRoll;
	
	public GameObject playerPrefab;
	
	// Use this for initialization
	void Start () {
        MusicManager.Instance.playPaletteTownMusic();
		Pokemon[] starters = PlayerSetupController.starters;
        string[] playerNames = PlayerSetupController.playerNames;
        if (starters == null || starters.Length == 0) {
            starters = new Pokemon[] { Pokemon.charmander };
            playerNames = new string[] { "Player X" };
        }
        setupPlayers(starters, playerNames);
		goldSquares = new List<int>();
		int count = 0;
		foreach(TileController tile in squares){
			tile.setup(this);
			if(tile.IS_GOLD){
				goldSquares.Add(count);
			}
			count++;
		}
        GUIController.Instance.DisplayBasicButton("Start Game", startGame);
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
		cameraController.FocusOnPlayer(getCurrentPlayer());
		getCurrentPlayer().takeTurn(nextPlayer);
	}
	
	public PlayerController getCurrentPlayer(){
		return players[currentPlayerNumber-1];
	}
	
	public TileController getCurrentTile(){
		return squares[getCurrentPlayer().currentTileNumber];
	}
	
	public Vector3 getFreeSpace(int tileNumber){
		Vector3 squarePosition = squares[tileNumber].transform.position;
		return squarePosition;
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
	
	public bool isPassingGoldSquare(int oldTileNum, int newTileNum, int skipable){
		for(int i = skipable; i < goldSquares.Count; i++){
			if(oldTileNum < goldSquares[i] && newTileNum > goldSquares[i]){
				return true;
			}
		}
		return false;
	}
	
	public int getNextGoldSquare(int oldTileNum){
		for(int i = 0; i < goldSquares.Count; i++){
			if(oldTileNum < goldSquares[i]){
				return goldSquares[i];
			}
		}
		return 0;
	}

    public void endGame() {
        GUIController.Instance.DisplayBasicModal(getCurrentPlayer().getName() + " wins!", () => {
            Application.LoadLevel(0);
        });
    }

    public int lastTileNum() {
        return this.squares.Length - 1;
    }
	
	
}

