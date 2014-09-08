using UnityEngine;
using System.Collections;

public abstract class TileController : MonoBehaviour {

	public virtual bool IS_GOLD { get {return false; }}

	protected GameController gameController;
	
	protected DicerollController roller;
	
	public void setup(GameController controller){
		gameController = controller;
		roller = new DicerollController(gameController);
	}
	
	public void applyRules(){
		doRules();
	}
	
	protected abstract void doRules();
	
	protected void returnControlToPlayer(){
		gameController.getCurrentPlayer().endTurn();
	}
}
