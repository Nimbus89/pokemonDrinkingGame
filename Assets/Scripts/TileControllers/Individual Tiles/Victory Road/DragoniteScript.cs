using UnityEngine;
using System.Collections;

public class DragoniteScript : BasicModalTileController
{

    override protected void doRules()
    {
        gameController.getCurrentPlayer().skipTurns(1, "You must recharge after Hyper Beam.");
        base.doRules();
    }


    protected override string getModalMessage()
    {
        return "Dragonite used Hyper Beam! Give 5 drinks, but lose a turn to recharge.";
    }
}

