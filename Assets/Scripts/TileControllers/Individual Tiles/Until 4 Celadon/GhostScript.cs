using UnityEngine;
using System.Collections;

public class GhostScript : TileController {

    public SilphCoScript silphCo;

    protected override void doRules()
    {
        if (silphCo.IsPlayerInZone())
        {
            GUIController.Instance.DisplayBasicModal("Someone is in Silph Co.! You use the Silph Scope to beat the Ghost! Everyone else drinks.", () =>
            {
                returnControlToPlayer();
            });
        }
        else 
        {
            GUIController.Instance.DisplayBasicModal("Noone is in Silph Co.! You can't get a Silph Scope. Take 3 drinks to appease the dead.", () =>
            {
                returnControlToPlayer();
            });
        }
    }
}
