using UnityEngine;
using System.Collections;

public abstract class PickPlayerTileController : TileController {

    protected override void doRules()
    {
        GUIController.Instance.DisplayBasicDialogs(new string[] { initialModalText() }, () =>
        {

            StartCoroutine(PlayerPickerManager.Instance.ShowButtons(reactToPlayerPicked, playersToPickFrom()));

        }, false);
    }

    protected abstract string initialModalText();

    protected abstract PlayerController[] playersToPickFrom();

    protected abstract void reactToPlayerPicked(PlayerController player);
}
