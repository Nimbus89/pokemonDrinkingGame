using UnityEngine;
using System.Collections;

public class HaunterScript : PickPlayerTileController
{

    protected override string initialModalText()
    {
        return "Haunter used Dream Eater! Devour someone else's dreams by moving them back 6 spaces.";
    }

    protected override PlayerController[] playersToPickFrom()
    {
        return gameController.players;
    }

    protected override void reactToPlayerPicked(PlayerController player)
    {
        DialogManager.Instance.enabled = false;
        CameraController.Instance.FocusOnPlayer(player);
        player.MoveBack(6, () =>
        {
            CameraController.Instance.FocusOnPlayer(gameController.getCurrentPlayer());
            returnControlToPlayer();
        });
    }
}
