using UnityEngine;
using System.Collections;

public class VermillionGymScript : DicerollTileController
{

    public override bool IS_GOLD { get { return true; } }

	protected override string initialModalText(){
		return "You challenge Gym Leader Surge!";
	}
	
	protected override void reactToDiceRoll(int rollResult){
		if((rollResult % 2) == 0){
            gameController.getCurrentPlayer().skipTurns(1, "Can't move due to paralysis!");
			gui.displayBasicModal("You're paralyzed! Take 2 drinks and miss a turn.", returnControlToPlayer);
		} else {
			gui.displayBasicModal("Take a drink.", returnControlToPlayer);
		}
	}
}

