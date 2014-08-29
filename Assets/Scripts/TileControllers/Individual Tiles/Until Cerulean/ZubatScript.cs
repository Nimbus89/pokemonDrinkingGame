using UnityEngine;
using System.Collections;

public class ZubatScript : BasicModalTileController
{

	private int roll;

	override protected string getModalMessage(){
		return "Zubats, they're everywhere! Take a drink!";
	}
	
	override protected void afterModal(){
		PlayerController player = this.gameController.getCurrentPlayer();
		player.rollReplaceAfterEffect = new ZubatAftereffectController(player, this.gameController);
		returnControlToPlayer();
	}
}

