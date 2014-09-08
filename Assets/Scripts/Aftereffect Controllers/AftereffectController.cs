using UnityEngine;
using System.Collections;

public abstract class AftereffectController
{

	protected PlayerController player;
	protected DicerollController roller;

	public AftereffectController(PlayerController player, GameController game){
		this.player = player;
		this.roller = new DicerollController(game);
	}
	
	public abstract void applyEffect();



}

