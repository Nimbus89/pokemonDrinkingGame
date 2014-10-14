using UnityEngine;
using System.Collections;

public class Gary3Script : DicerollTileController
{

    protected override string initialModalText()
    {
        return "You challenge Rival Gary! Roll a die.";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        GUIController.Instance.DisplayDialog("Take " + rollResult + " drinks.", returnControlToPlayer);
    }

}
