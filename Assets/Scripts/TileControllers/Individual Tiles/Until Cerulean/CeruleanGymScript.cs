using UnityEngine;
using System.Collections;

public class CeruleanGymScript : BasicModalTileController
{

	public override bool IS_GOLD { get {return true; }}

	override protected string getModalMessage(){
		return "You challenge Gym Leader Misty! Take one drink, or take two, give one!";
	}
}

