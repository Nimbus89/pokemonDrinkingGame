using UnityEngine;
using System.Collections;

public class GayScript : DicerollTileController {
	
	protected override string initialModalText(){
		return "You challenge Rival Gary!";
	}
	
	protected override void reactToDiceRoll(int rollResult){
		int drinks = Mathf.CeilToInt(rollResult/2);
		gui.displayBasicModal("Give " + drinks + " drinks, take " + drinks + " drinks.", returnControlToPlayer);
	}
	
}
