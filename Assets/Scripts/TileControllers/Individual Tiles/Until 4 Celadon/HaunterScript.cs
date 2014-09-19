using UnityEngine;
using System.Collections;

public class HaunterScript : TileController
{
    protected override void doRules()
    {

        GUIController.Instance.DisplayBasicDialogs(new string[] { "Haunter used Dream Eater! Devour someone else's dreams by moving them back 6 spaces." }, () =>
        {

            StartCoroutine(PlayerPickerManager.Instance.ShowButtons((PlayerController player) =>
            {
                DialogManager.Instance.enabled = false;
                CameraController.Instance.FocusOnPlayer(player);
                player.MoveBack(6, () =>
                {
                    CameraController.Instance.FocusOnPlayer(gameController.getCurrentPlayer());
                    returnControlToPlayer();
                });
            }, gameController.players));

        }, false);


    }
}
