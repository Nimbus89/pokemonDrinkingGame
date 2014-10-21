using UnityEngine;
using System.Collections;

public class StartMenuManager : BaseGUIManager<StartMenuManager>
{

    private Rect windowRect = new Rect(virtualWidth - virtualWidth/4, 0, virtualWidth/4, virtualHeight);

    public void TogglePause() {
        if (this.enabled) {
            this.UnPause();
        } else {
            this.Pause();
        }
    }   

    public void Pause(){
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
        if (GUI.Button(new Rect(0, 0, windowRect.width, 100), "Resume")) {
            this.UnPause();
        }
    }

}
