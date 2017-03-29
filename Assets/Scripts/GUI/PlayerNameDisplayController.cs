using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameDisplayController : BaseGUIManager<PlayerNameDisplayController>
{

    private Rect ModalWindowPosition = new Rect(100, 0, virtualWidth - 100, 50);
    private string playerName = "";



    public override void OnGUI()
    {
        base.OnGUI();
        if (playerName != "" && Time.timeScale > 0.0f)
        {
            GUI.Box(ModalWindowPosition, playerName);
        }
    }

    public void setPlayerName(string playerName) {
        this.playerName = playerName;
    }
}
