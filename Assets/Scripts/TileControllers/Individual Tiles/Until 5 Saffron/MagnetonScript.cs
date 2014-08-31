using UnityEngine;
using System.Collections;

public class MagnetonScript : DicerollTileController {
	
	protected override string initialModalText(){
		return "Scientist sends out Magneton! Roll a die.";
	}
	
	protected override void reactToDiceRoll(int rollResult){
		if((rollResult % 2) == 0){
			gui.displayBasicModal("You attract one drink per player in the game.", returnControlToPlayer);
		} else {
			gui.displayBasicModal("You repel one drink to each player in the game.", returnControlToPlayer);
		}
		
	}
	
}
