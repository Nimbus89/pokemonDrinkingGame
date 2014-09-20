using UnityEngine;
using System.Collections;

public abstract class TileController : MonoBehaviour {

	public virtual bool IS_GOLD { get {return false; }}

	protected GameController gameController;
	
	protected DicerollController roller;

    protected int myTileNum;
	
	public void setup(GameController controller, int tileNum){
		gameController = controller;
		roller = new DicerollController(gameController);
        this.myTileNum = tileNum;
	}
	
	public void applyRules(){
		doRules();
	}
	
	protected abstract void doRules();
	
	protected void returnControlToPlayer(){
		gameController.getCurrentPlayer().endTurn();
	}

    public PlayerController[] GetPlayersOnMe() {
        return gameController.GetPlayersOnTile(myTileNum);
    }
}
