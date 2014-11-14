using UnityEngine;
using System.Collections;

public class StartMenuManager : BaseGUIManager<StartMenuManager>
{

    private Rect windowRect = new Rect(virtualWidth - virtualWidth/4, 0, virtualWidth/4, virtualHeight);
    const int buttonHeight = 60;
    const int buttonLeft = 10;
    private GUIStyle buttonStyle;

    public override void Awake()
    {

        base.Awake();
        GUISkin skin = Resources.Load<GUISkin>("Skin");
        buttonStyle = new GUIStyle(skin.button);
        buttonStyle.normal.background = null;
        buttonStyle.focused.background = null;
        buttonStyle.active.background = null;
        buttonStyle.hover.background = null;
        buttonStyle.alignment = TextAnchor.MiddleLeft;
        buttonStyle.fontSize = 12;
    }

    public void TogglePause() {
        if (this.enabled) {
            this.UnPause();
        } else {
            this.Pause();
        }
    }   

    public void Pause(){
        SFXManager.Instance.playClack();
        Time.timeScale = 0.0f;
        this.enabled = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1.0f;
        this.enabled = false;
    }

    public override void OnGUI(){
        base.OnGUI();
        GUI.ModalWindow(1, windowRect, drawWindow, "");
    }

    private void drawWindow(int id) {
        if (GUI.Button(new Rect(buttonLeft, buttonHeight * 0, windowRect.width, buttonHeight), "Resume", buttonStyle))
        {
            Debug.Log("Resume");
            this.UnPause();
        }
        if (GUI.Button(new Rect(buttonLeft, buttonHeight * 1, windowRect.width, buttonHeight), "Quit", buttonStyle))
        {
            Debug.Log("Quit");
            Application.LoadLevel(0);
            UnPause();
        }
        if (GUI.Button(new Rect(buttonLeft, buttonHeight * 2, windowRect.width, buttonHeight), "Mute", buttonStyle))
        {
            Debug.Log("Mute");
            MusicManager manager = MusicManager.Instance;
            if (manager) {
                manager.ToggleMute();
            }
        }
        if (GUI.Button(new Rect(buttonLeft, buttonHeight * 3, windowRect.width, buttonHeight), "FScr.", buttonStyle))
        {
            Debug.Log("FS");
            Screen.fullScreen = !Screen.fullScreen;
        }
        string rdmtext;
        if (GameController.realMode)
        {
            rdmtext = "RDM ON";
        }
        else {
            rdmtext = "RDM OFF";
        }
        if (GUI.Button(new Rect(buttonLeft, buttonHeight * 4, windowRect.width, buttonHeight), rdmtext, buttonStyle))
        {
            Debug.Log("RDM.");
            GameController.realMode = !GameController.realMode;
        }


    }

}
