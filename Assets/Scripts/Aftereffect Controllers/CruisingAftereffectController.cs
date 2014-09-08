using UnityEngine;
using System.Collections;

public class CruisingAftereffectController : AftereffectController
{

	private int duration;
	
	public CruisingAftereffectController(PlayerController player, GameController game, int duration) : base(player, game){
		this.duration = duration;
	}
	
	public override void applyEffect(){
		duration--;
		if(duration == 0){
            player.rollReplaceAfterEffect = null;
		}
		roller.doDiceRollWithMessage("You're on a cruise! Can't move this turn. Roll a die.", reactToRoll);
	}
	
	private void reactToRoll(int result){
        GUIController.Instance.DisplayBasicModal("The cruise has a full bar! Take " + result + " drinks.", player.endTurn);
	}
}

