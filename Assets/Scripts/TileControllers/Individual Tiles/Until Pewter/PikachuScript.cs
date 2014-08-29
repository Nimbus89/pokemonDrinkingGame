using UnityEngine;
using System.Collections;

public class PikachuScript : BasicModalTileController {

	override protected string getModalMessage(){
		return "You traded in your starter for a Pikachu. Drink 2.";
	}

	override protected void afterModal(){
		gameController.getCurrentPlayer().pokeController.becomePikachi();
		base.afterModal();
	}

}
