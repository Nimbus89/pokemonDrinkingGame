using UnityEngine;
using System.Collections;

public class ClefairyScript : BasicModalTileController {

	override protected string getModalMessage(){
		return "Clefairy used Metronome! A random tile effect will now happen!";
	}
	
	override protected void afterModal(){
		gameController.getRandomTile().applyRules();
	}
	
}
