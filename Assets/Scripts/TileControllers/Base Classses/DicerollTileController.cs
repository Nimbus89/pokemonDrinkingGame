using UnityEngine;
using System.Collections;

public abstract class DicerollTileController : TileController {
	
	protected override void doRules(){
		roller.doDiceRollWithMessage(initialModalText(), reactToDiceRoll);
	}
	
	protected abstract string initialModalText();
	
	protected abstract void reactToDiceRoll(int rollResult);
	
}
