using UnityEngine;
using System.Collections;

public class LegendaryBirdsScript : DicerollTileController
{

    public override bool CAN_BE_COPIED { get { return false; } }

    public override bool IS_GOLD { get { return true; } }

    protected override string initialModalText()
    {
        return "You encounter a Legendary Bird! Gotta catch 'em all! Roll a die.";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        if (rollResult < 4)
        {
            throwGreatBall();
        }
        else 
        {
            throwUltraBall();
        }
    }

    private void throwGreatBall() {
        GUIController.Instance.DisplayBasicModal("Why are you throwing Great Balls at it? Take a drink!", applyAfterEffect);
    }

    private void throwUltraBall() {
        PlayerController player = gameController.getCurrentPlayer();
        GUIController.Instance.DisplayBasicModal("You got one! Everyone drinks in celebration!", () => {
            player.legendaryBirds++;
            applyAfterEffect();
        });
        
    }

    private void applyAfterEffect() {
        PlayerController player = gameController.getCurrentPlayer();
        if (player.legendaryBirds < 3)
        {
            GUIController.Instance.DisplayBasicModal("You can't move on until you have caught all three!", () =>
            {
                player.rollReplaceAfterEffect = new DoOverAftereffectController(player, gameController);
                returnControlToPlayer();
            });
        }
        else 
        {
            GUIController.Instance.DisplayBasicModal("You did it, you caught all three! Now, on toward the POKeMON League!", () =>
            {
                player.rollReplaceAfterEffect = null;
                returnControlToPlayer();
            });
        }
    }
}
