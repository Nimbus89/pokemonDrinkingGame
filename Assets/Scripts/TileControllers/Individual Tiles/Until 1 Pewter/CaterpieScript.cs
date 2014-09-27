using UnityEngine;
using System.Collections;

public class CaterpieScript : BasicModalTileController {

	override protected string getModalMessage(){
		return "Caterpie used String Shot! Until your next turn, every player moves half of thier dice roll.";
	}
	
	override protected void afterModal(){
		foreach(PlayerController player in gameController.players){
			if(player != gameController.getCurrentPlayer()){
				player.stringShotMe();
			}
		}
		base.afterModal();
	}
}
