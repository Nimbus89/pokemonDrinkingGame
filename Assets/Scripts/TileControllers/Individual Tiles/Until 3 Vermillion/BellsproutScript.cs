using UnityEngine;
using System.Collections;

public class BellsproutScript : DicerollTileController
{

	override protected string initialModalText(){
		return "Bellsprout used Wrap! Roll a die.";
	}
	
	protected override void reactToDiceRoll(int rollResult){
		PlayerController player = gameController.getCurrentPlayer();
		player.startOfTurnEffects.Add(new WrapAftereffectController(player, gameController, rollResult));
        GUIController.Instance.DisplayDialog("For " + rollResult + " turns, you must dirnk at the start of your turn.", returnControlToPlayer);
	}
}

