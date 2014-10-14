using UnityEngine;
using System.Collections;

public class LaprasScript : PickPlayerTileController
{

    protected override string initialModalText()
    {
        return "Lapras used Confuse Ray! Pick a player.";
    }

    protected override PlayerController[] playersToPickFrom()
    {
        return gameController.players;
    }

    protected override void reactToPlayerPicked(PlayerController player)
    {
        player.startOfTurnEffects.Add(new ConfusedAftereffectController(player, gameController));
        GUIController.Instance.DisplayDialog(player.getName() + " is now confused!", returnControlToPlayer);
    }
}
