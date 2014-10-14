using UnityEngine;
using System.Collections;

public abstract class BasicModalTileController : TileController {

	override protected void doRules(){
        GUIController.Instance.DisplayDialog(getModalMessage(), afterModal);
	}
	
	abstract protected string getModalMessage();
	
	virtual protected void afterModal(){
		returnControlToPlayer ();
	}
}
