using UnityEngine;
using System.Collections;

public class ZubatScript : BasicModalTileController
{

	override protected string getModalMessage(){
		return "Zubats, they... they're everywhere! Take a drink!";
	}
	
	override protected void afterModal(){
		PlayerController player = this.gameController.getCurrentPlayer();
		player.rollReplaceAfterEffect = new ZubatAftereffectController(player, this.gameController);
		returnControlToPlayer();
	}
}

