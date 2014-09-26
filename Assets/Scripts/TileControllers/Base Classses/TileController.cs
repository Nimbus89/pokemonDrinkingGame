using UnityEngine;
using System.Collections;

public abstract class TileController : MonoBehaviour {

    public AudioClip tileMusic;

	public virtual bool IS_GOLD { get {return false; }}

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
}
