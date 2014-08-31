using UnityEngine;
using System.Collections;

public class MagikarpScript : BasicModalTileController {

    override protected void doRules()
    {
        gameController.getCurrentPlayer().getMagikarp();
        base.doRules();
    }
    
    override protected string getModalMessage(){
		return "Magikarp used Splash! ...but nothing happened...";
	}
}
