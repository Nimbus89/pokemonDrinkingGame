using UnityEngine;
using System.Collections;

public class EliteFourScript : DicerollTileController
{

    public override bool IS_GOLD { get { return true; } }

    protected override string initialModalText()
    {
        return "Challenge the land's greatest trainers: the Elite Four! Roll a die.";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        PlayerController player = gameController.getCurrentPlayer();
        if (rollResult == 4)
        {
            player.rollReplaceAfterEffect = null;
            GUIController.Instance.DisplayBasicModal("You did it! All that grinding paid off. Next you will face the League Champ!", () => {
                returnControlToPlayer();
            });
        }
        else 
        {
            player.rollReplaceAfterEffect = new DoOverAftereffectController(player, gameController);
            GUIController.Instance.DisplayBasicModal("You get crushed! Better go grind on Victory Road some more. Drink 4.", () =>
            {
                returnControlToPlayer();
            });
        }
    }
}
