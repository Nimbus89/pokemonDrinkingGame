using UnityEngine;
using System.Collections;

public class ZubatAftereffectController : AftereffectController
{

	private int roll;

	public ZubatAftereffectController(PlayerController player, GameController game) : base(player, game){}

	public override void applyEffect(){

		roller.doDiceRollWithMessage("Zubats, they... they're everywhere! Roll more than 2 to escape!", reactToDiceRoll);

	}
	
	private void reactToDiceRoll(int rollResult){
		roll = rollResult;
		if(rollResult < 3){
            GUIController.Instance.DisplayBasicModal("You couldn't escape!", player.endTurn);
		} else {
            GUIController.Instance.DisplayBasicModal("You escaped!", escape);
		}
	}
	
	private void escape(){
		player.rollReplaceAfterEffect = null;
		player.move(roll);
	}
}

