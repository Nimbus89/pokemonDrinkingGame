using UnityEngine;
using System.Collections;

public class ClefairyScript : BasicModalTileController {

	override protected string getModalMessage(){
		return "Clefairy used Metronome! Tick Tock Tick Tock.";
	}
	
	override protected void afterModal(){
        TileController tile = gameController.getRandomTile();
		while(!tile.CAN_BE_COPIED){
            tile = gameController.getRandomTile();
        }
        tile.applyRules(currentPlayer);
	}
	
}
