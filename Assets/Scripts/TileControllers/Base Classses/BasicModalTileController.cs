using UnityEngine;
using System.Collections;

public abstract class BasicModalTileController : TileController {

	override protected void doRules(){
		gui.displayBasicModal(getModalMessage(), afterModal);
	}
	
	abstract protected string getModalMessage();
	
	virtual protected void afterModal(){
		returnControlToPlayer ();
	}
}
