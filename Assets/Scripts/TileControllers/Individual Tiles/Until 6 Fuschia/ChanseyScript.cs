using UnityEngine;
using System.Collections;

public class ChanseyScript : DicerollTileController, StartOfTurnEffectTileController
{
    public SafariZoneScript safariZone;


    protected override string initialModalText()
    {
        return "A wild Chansey appeared! Roll a die.";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        if (rollResult < 4)
        {
            GUIController.Instance.DisplayDialog("Chansey eludes you, drink 1.", returnControlToPlayer);
        }
        else
        {
            GUIController.Instance.DisplayDialog("You capture Chansey! Give 2 drinks.", returnControlToPlayer);
        }
    }

    public void doStartOfTurnRules(PlayerController player, CallbackDelegate cb)
    {
        safariZone.doSafariRules(player, cb);
    }
}
