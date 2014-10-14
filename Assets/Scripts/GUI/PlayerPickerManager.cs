using UnityEngine;
using System.Collections;

public class PlayerPickerManager : BaseGUIManager<PlayerPickerManager>
{

    private int scrollAreaMargins = 50;
    private int scrollAreaLeft;
    private int scrollAreaTop;
    private float scrollAreaWidth;
    private float scrollAreaHeight;
    private int buttonHeight = 100;
    private PlayerController[] players;
    private Vector2 scrollPosition = Vector2.zero;
    private PlayerCallbackDelegate callback;

    public override void Awake()
    {
        scrollAreaLeft = scrollAreaMargins;
        scrollAreaTop = scrollAreaMargins;
        scrollAreaWidth = virtualWidth - scrollAreaMargins * 2;
        scrollAreaHeight = virtualHeight - (scrollAreaMargins * 2 + 100);

        base.Awake();
    }

    public IEnumerator ShowButtons(PlayerCallbackDelegate cb, PlayerController[] players)
    {
        this.players = players;
        this.enabled = true;
        this.callback = cb;
        while (this.enabled)
        {
            yield return 0;
        }
    }

    
    void OnGUI()
    {
        base.OnGUI();
        int numPlayers = players.Length;
        scrollPosition = GUI.BeginScrollView(new Rect(scrollAreaLeft, scrollAreaTop, scrollAreaWidth, scrollAreaHeight), scrollPosition, new Rect(0, 0, 100, buttonHeight * numPlayers));

        int count = 0;
        foreach(PlayerController player in players){
            if (GUI.Button(new Rect(0, buttonHeight * count, scrollAreaWidth, buttonHeight), player.getName())) {
                finished(player);
            }
            count++;
        }
        GUI.EndScrollView();
    }

    private void finished(PlayerController player) {
        this.enabled = false;
        this.callback(player);
    }
}
