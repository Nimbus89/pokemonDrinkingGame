using UnityEngine;
using System.Collections;

public class DratiniScript : DicerollTileController, StartOfTurnEffectTileController
{

    public SafariZoneScript safariZone;



    protected override string initialModalText()
    {
        return "... Oh! A bite! A wild Dratini appeared! Roll a 1 to catch it!";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        if (rollResult == 1)
        {
            GUIController.Instance.DisplayBasicModal("You caught a Dratini!", returnControlToPlayer);
        }
        else 
        {
            GUIController.Instance.DisplayBasicModal("Aww, it got away! Drink 1.", returnControlToPlayer);
        }
    }

    public void doStartOfTurnRules(PlayerController player, CallbackDelegate cb)
    {
        safariZone.doSafariRules(player, cb);
    }
}
