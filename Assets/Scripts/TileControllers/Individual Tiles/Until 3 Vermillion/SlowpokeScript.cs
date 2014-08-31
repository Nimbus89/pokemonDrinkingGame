using UnityEngine;
using System.Collections;

public class SlowpokeScript : BasicModalTileController
{

	override protected string getModalMessage(){
		return "Slowpoke is slow... For the first person here, make up a gesture. For the rest of the game (and once per turn rotation), "
		+ "when you do the gesture, the last person to mimic you takes a drink.";
	}
}

