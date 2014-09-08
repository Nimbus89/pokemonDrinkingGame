using UnityEngine;
using System.Collections;

public class CeladonGymScript : DicerollTileController {

    public override bool IS_GOLD { get { return true; } }

    protected override string initialModalText()
    {
        return "You challenge Gym Leader Erika! Roll a die.";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        if (rollResult < 3) 
        {
            stunSpore();
        }
        else if (rollResult < 5)
        {
            vineWhip();
        }
        else 
        {
            megaDrain();
        }
    }

    private void megaDrain()
    {
        GUIController.Instance.DisplayBasicModal("Mega Drain! Finish your drink!", returnControlToPlayer);
    }

    private void vineWhip()
    {
        GUIController.Instance.DisplayBasicModal("Vine Whip! Take 2 drinks, give 2 drinks.", returnControlToPlayer);
    }

    private void stunSpore()
    {
        gameController.getCurrentPlayer().skipTurns(1, "You are stunned! Miss a turn.");
        GUIController.Instance.DisplayBasicModal("Stun Spore! You lose a turn.", returnControlToPlayer);
    }
}
