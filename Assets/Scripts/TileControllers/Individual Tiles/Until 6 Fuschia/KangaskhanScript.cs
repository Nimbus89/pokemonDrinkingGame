using UnityEngine;
using System.Collections;

public class KangaskhanScript : DicerollTileController, StartOfTurnEffectTileController
{
    public SafariZoneScript safariZone;


    protected override string initialModalText()
    {
        return "A wild Kangaskhan appeared! Roll a die.";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        if (rollResult < 6)
        {
            GUIController.Instance.DisplayBasicModal("Shit! Kangaskhan got away! Drink 3 in depression.", returnControlToPlayer);
        }
        else
        {
            GUIController.Instance.DisplayBasicModal("Hell yes! You caught him! Give 3 drinks.", returnControlToPlayer);
        }
    }

    public void doStartOfTurnRules(PlayerController player, CallbackDelegate cb)
    {
        safariZone.doSafariRules(player, cb);
    }
}
