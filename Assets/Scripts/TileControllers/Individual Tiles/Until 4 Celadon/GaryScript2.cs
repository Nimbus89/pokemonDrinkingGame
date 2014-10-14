using UnityEngine;
using System.Collections;

public class GaryScript2 : DicerollTileController {
	
	protected override string initialModalText(){
		return "You challenge Rival Gary!";
	}
	
	protected override void reactToDiceRoll(int rollResult){
		int drinks = rollResult;
        if (drinks == 1) {
            drinks = 2;
        }
        drinks -= 2;

        GUIController.Instance.DisplayDialog("Give 2 drinks, take " + drinks + " drinks.", returnControlToPlayer);
	}
	
}
