using UnityEngine;
using System.Collections;

public class PidgeyScript : BasicModalTileController {

	override protected string getModalMessage(){
		return "Pidgey used Quick Attack! Use that quickness to give one drink and take an extra turn.";
	}
	
	override protected void afterModal(){
		gameController.getCurrentPlayer().getExtraTurns(1);
		base.afterModal();
	}
}
