using UnityEngine;
using System.Collections;

public class ChampGaryTileController : TileController
{

    public override bool IS_GOLD { get { return true; } }

    public override bool CAN_BE_COPIED { get { return false; } }

    protected override void doRules()
    {
        gameController.getCurrentPlayer().rollReplaceAfterEffect = new ChampionGaryAftereffectController(gameController.getCurrentPlayer(), gameController);
        GUIController.Instance.DisplayBasicModal("You face the newly crowned Champion, Gary! Finish a FULL drink to take this bastard down for the last time!"
            + " You cannot move and your turns are skipped until your drink is finished.", () => {
                returnControlToPlayer();
        });
    }
}
