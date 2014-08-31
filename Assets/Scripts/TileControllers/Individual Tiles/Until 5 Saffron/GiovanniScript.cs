using UnityEngine;
using System.Collections;

public class GiovanniScript : DicerollTileController
{

    protected override string initialModalText()
    {
        return "You challenge Rocket Leader Giovanni! Roll a die.";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        if (rollResult < 4)
        {
            gui.displayBasicModal("Give " + rollResult + " drinks.", returnControlToPlayer);
        }
        else
        {
            gui.displayBasicModal("Take " + rollResult + " drinks.", returnControlToPlayer);
        }

    }

}
