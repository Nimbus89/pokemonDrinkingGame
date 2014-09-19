using UnityEngine;
using System.Collections;

public class PlayerPickerManager : MonoBehaviour {

    private int scrollAreaMargins = 50;
    private int scrollAreaLeft;
    private int scrollAreaTop;
    private int scrollAreaWidth;
    private int scrollAreaHeight;
    private int buttonHeight = 100;
    private PlayerController[] players;
    private Vector2 scrollPosition = Vector2.zero;
    private PlayerCallbackDelegate callback;

    public static PlayerPickerManager Instance
    {
        get { return instance; }
    }

    private static PlayerPickerManager instance;

    public void Awake()
    {

    scrollAreaLeft = scrollAreaMargins;
    scrollAreaTop = scrollAreaMargins;
    scrollAreaWidth = Screen.width - scrollAreaMargins * 2;
    scrollAreaHeight = Screen.height - (scrollAreaMargins * 2 + 100);

        this.enabled = false;
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
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
        GUI.skin = GUIController.Instance.skin;
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
