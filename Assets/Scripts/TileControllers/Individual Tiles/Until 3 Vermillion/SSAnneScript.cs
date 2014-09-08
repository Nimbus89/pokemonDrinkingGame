using UnityEngine;
using System.Collections;

public class SSAnneScript : DicerollTileController {

	override protected string initialModalText(){
		return "You take a cruise on the S.S. Anne! Roll a die.";
	}
	
	protected override void reactToDiceRoll(int rollResult){
		PlayerController player = gameController.getCurrentPlayer();
		player.rollReplaceAfterEffect = new CruisingAftereffectController(player, gameController, rollResult);
        GUIController.Instance.DisplayBasicModal("For " + rollResult + " turns, you must dirnk at the start of your turn.", returnControlToPlayer);
	}
}
