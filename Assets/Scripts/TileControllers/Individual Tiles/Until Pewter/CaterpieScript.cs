using UnityEngine;
using System.Collections;

public class CaterpieScript : BasicModalTileController {

	override protected string getModalMessage(){
		return "Everyone else moves half for one turn.";
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
