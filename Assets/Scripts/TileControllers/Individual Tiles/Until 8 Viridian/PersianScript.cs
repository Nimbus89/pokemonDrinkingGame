using UnityEngine;
using System.Collections;

public class PersianScript : DicerollTileController {

    protected override string initialModalText()
    {
        return "Persian used Fury Swipes! Roll a die.";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        GUIController.Instance.DisplayBasicModal("Critical hit! Give " + rollResult*2 + " drinks.", returnControlToPlayer);
    }
}
