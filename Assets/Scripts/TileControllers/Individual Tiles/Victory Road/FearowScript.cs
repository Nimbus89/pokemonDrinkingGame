using UnityEngine;
using System.Collections;

public class FearowScript : BasicModalTileController {


    public override bool CAN_BE_COPIED { get { return false; } }

    protected override string getModalMessage()
    {
        return "Fearow used Mirrow Move!";
    }

    override protected void afterModal()
    {
        PlayerController previousPlayer = gameController.getPreviousPlayer();
        TileController tile = previousPlayer.getCurrentTile();
        if (!tile.CAN_BE_COPIED)
        {
            GUIController.Instance.DisplayBasicModal("But it failed! Take 3 drinks instead.", returnControlToPlayer);
        }
        else 
        {
            tile.applyRules(currentPlayer);
        }
    }
}
