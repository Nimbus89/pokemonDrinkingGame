﻿using UnityEngine;
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
            GUIController.Instance.DisplayDialog("Give " + rollResult + " drinks.", returnControlToPlayer);
        }
        else
        {
            GUIController.Instance.DisplayDialog("Take " + rollResult + " drinks.", returnControlToPlayer);
        }

    }

}
