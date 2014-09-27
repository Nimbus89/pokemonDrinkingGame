using UnityEngine;
using System.Collections;

public class ClefairyScript : BasicModalTileController {

	override protected string getModalMessage(){
		return "Clefairy used Metronome! Tick Tock Tick Tock.";
	}
	
	override protected void afterModal(){
		gameController.getRandomTile().applyRules(currentPlayer);
	}
	
}
