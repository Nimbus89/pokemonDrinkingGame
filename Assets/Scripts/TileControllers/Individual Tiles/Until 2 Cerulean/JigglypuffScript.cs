using UnityEngine;
using System.Collections;

public class JigglypuffScript : BasicModalTileController {

	override protected string getModalMessage(){
		return "Jigglypuff used sing! Everyone else fell asleep! Take an extra turn. "
		+ "It is now your job to draw on anybody that falls asleep for real!";
	}
	
	override protected void afterModal(){
        gameController.getCurrentPlayer().getExtraTurns(1);
		base.afterModal();
	}
}
