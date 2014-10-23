using UnityEngine;
using System.Collections;

public class StartMenuManager : BaseGUIManager<StartMenuManager>
{

    private Rect windowRect = new Rect(virtualWidth - virtualWidth/4, 0, virtualWidth/4, virtualHeight);
    private int buttonHeight = 70;


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
        if (GUI.Button(new Rect(0, buttonHeight*0, windowRect.width, buttonHeight), "Resume"))
        {
            this.UnPause();
        }
        if (GUI.Button(new Rect(0, buttonHeight*1, windowRect.width, buttonHeight), "Quit"))
        {
            Application.LoadLevel(0);
            UnPause();
        }
        if (GUI.Button(new Rect(0, buttonHeight * 2, windowRect.width, buttonHeight), "Mute"))
        {
            MusicManager manager = MusicManager.Instance;
            if (manager) {
                manager.ToggleMute();
            }
        }
        if (GUI.Button(new Rect(0, buttonHeight * 3, windowRect.width, buttonHeight), "Fullscreen"))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

}
