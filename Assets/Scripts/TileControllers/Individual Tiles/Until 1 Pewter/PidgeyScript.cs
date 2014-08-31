using UnityEngine;
using System.Collections;

public class PidgeyScript : BasicModalTileController {

	override protected string getModalMessage(){
		return "Give 1 Drink, take an extra turn.";
	}
	
	override protected void afterModal(){
		gameController.getCurrentPlayer().getExtraTurns(1);
		base.afterModal();
	}
}
