using UnityEngine;
using System.Collections;

public class PauseButtonManager : BaseGUIManager<PauseButtonManager>
{

    private static int BUTTON_WIDTH = 100;
    private static int BUTTON_HEIGHT = 50;
    private float BUTTON_VERTICAL_MARGIN;
    private float BUTTON_HORIZONTAL_MARGIN;

    private BooleanCallbackDelegate callback;

    public override void Awake()
    {
        base.Awake();
    }

    public void OnGUI()
    {        
        base.OnGUI();
        if (Time.timeScale > 0.0f) {
            if (GUI.Button(new Rect(0, 0, BUTTON_WIDTH, BUTTON_HEIGHT), "PAUSE"))
            {
                StartMenuManager.Instance.Pause();
            }
        }
    }
}
