using UnityEngine;
using System.Collections;

public class PewterGymScript : DicerollTileController {
	
	public override bool IS_GOLD { get {return true; }}
	
	protected override string initialModalText(){
		return "You challenge Gym Leader Brock!";
	}
	
	protected override void reactToDiceRoll(int rollResult){
		if((rollResult % 2) == 0){
            GUIController.Instance.DisplayDialog("Give a drink.", returnControlToPlayer);
		} else {
            GUIController.Instance.DisplayDialog("Take a drink.", returnControlToPlayer);
		}
		
	}
	
}
