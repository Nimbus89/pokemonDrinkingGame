using UnityEngine;
using System.Collections;

public class TaurusScript : BasicModalTileController, StartOfTurnEffectTileController
{

    public SafariZoneScript safariZone;


    public void doStartOfTurnRules(PlayerController player, CallbackDelegate cb)
    {
        safariZone.doSafariRules(player, cb);
    }

    protected override string getModalMessage()
    {
        return "A wild Taurus appeared... but instantly fled. Drink 2 for not being quick enough.";
    }
}
