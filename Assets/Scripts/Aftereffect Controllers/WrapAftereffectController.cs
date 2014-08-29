using UnityEngine;
using System.Collections;

public class WrapAftereffectController : AftereffectController
{
	private int duration;

	public WrapAftereffectController(PlayerController player, GameController game, int duration) : base(player, game){
		this.duration = duration;
	}
	
	public override void applyEffect(){
		duration--;
		if(duration == 0){
			player.startOfTurnEffects.Remove(this);
		}
		gui.displayBasicModal("Wrap deals damage! Take a drink!", player.handleNextStartOfTurnEffect);
	}
}

