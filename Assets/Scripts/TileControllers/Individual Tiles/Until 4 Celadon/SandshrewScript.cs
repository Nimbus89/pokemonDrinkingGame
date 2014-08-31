using UnityEngine;
using System.Collections;

public class SandshrewScript : BasicModalTileController
{
	override protected string getModalMessage(){
		return "Sandshrew used Sand-Attack! For the rest of the game you may only drink out of your dominant hand.";
	}
}

