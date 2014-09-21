using UnityEngine;
using System.Collections;

public class FearowScript : BasicModalTileController {


    protected override string getModalMessage()
    {
        return "Fearow used Mirrow Move!";
    }

    override protected void afterModal()
    {
        PlayerController previousPlayer = gameController.getPreviousPlayer();
        TileController tile = previousPlayer.getCurrentTile();
        if (tile is FearowScript)
        {
            GUIController.Instance.DisplayBasicModal("But it failed!", returnControlToPlayer);
        }
        else {
            tile.applyRules(currentPlayer);
        }
    }
}
