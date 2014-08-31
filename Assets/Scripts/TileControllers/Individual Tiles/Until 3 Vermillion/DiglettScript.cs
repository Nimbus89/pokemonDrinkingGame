using UnityEngine;
using System.Collections;

public class DiglettScript : BasicModalTileController
{
	override protected string getModalMessage(){
		return "Diglett used Dig! If your drink is less then half full, finish it. "
		 + "Otherwise, drink half, and make someone else drink half of theirs.";
	}
}

