using UnityEngine;
using System.Collections;

public abstract class TileController : MonoBehaviour {

    public AudioClip tileMusic;

	public virtual bool IS_GOLD { get {return false; }}

    public virtual bool CAN_BE_COPIED { get { return true; } }

	protected GameController gameController;
	
	protected DicerollController roller;

    protected int myTileNum;

    protected PlayerController currentPlayer;

    public void PlayMyMusic() {
        if (this.tileMusic != null) {
            MusicManager.Instance.PlayTileMusic(tileMusic);
        }
    }

	public void setup(GameController controller, int tileNum){
		gameController = controller;
		roller = new DicerollController(gameController);
        this.myTileNum = tileNum;
	}
	
	public void applyRules(PlayerController player){
        PlayMyMusic();
        currentPlayer = player;
        TypedTileScript typedTileScript = this.GetComponent<TypedTileScript>();
        if (typedTileScript != null)
        {
            typedTileScript.ApplyTypeRules(player.GetPokemonType(), doRules);
        }
        else 
        {
            doRules();
        }
	}
	
	protected abstract void doRules();
	
	protected void returnControlToPlayer(){
        currentPlayer.endTurn();
	}

    public PlayerController[] GetPlayersOnMe() {
        return gameController.GetPlayersOnTile(myTileNum);
    }

    public Vector3 getPlayerPosition(int playerNumber){

        float angle = (360 / (float)(gameController.GetNumberOfPlayers())) * (float)(playerNumber - 1);
        angle = angle + 90f;
        angle = (Mathf.PI / 180f) * angle;
        float radius = 1.3f;
        float xPos = getPosition().x;
        xPos += radius * Mathf.Cos(angle);
        float yPos = getPosition().y;
        yPos += radius * Mathf.Sin(angle);
        return new Vector3(xPos, yPos, getPosition().z);
    }

    private Vector3 getPosition() {
        return this.transform.position;
    } 
}
