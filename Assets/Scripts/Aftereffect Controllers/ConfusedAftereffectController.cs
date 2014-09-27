﻿using UnityEngine;
using System.Collections;

public class ConfusedAftereffectController : AftereffectController
{

    public ConfusedAftereffectController(PlayerController player, GameController game)
        : base(player, game)
    {
	}
	
	public override void applyEffect(){

        roller.doDiceRollWithMessage("Your POKeMON is confused! Roll a die.", (int result) =>

        {
            if (result >= 4)
            {
                player.startOfTurnEffects.Remove(this);
                GUIController.Instance.DisplayBasicModal("Your POKeMON recovered from it's confusion! Now roll to move.", player.handleNextStartOfTurnEffect);
            }
            else 
            {
                GUIController.Instance.DisplayBasicModal("Your POKeMON hurt itself in it's confusion! Drink 1 and miss a turn.", player.endTurn);
            }
        });
	}
}