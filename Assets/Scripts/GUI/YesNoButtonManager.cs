using UnityEngine;
using System.Collections;

public class YesNoButtonManager : BaseGUIManager<YesNoButtonManager>
{

    private static int BUTTON_INTERNAL_MARGIN = 30;
    private static int BUTTON_WIDTH = 100;
    private static int BUTTON_HEIGHT = 100;
    private float BUTTON_VERTICAL_MARGIN;
    private float BUTTON_HORIZONTAL_MARGIN;

    private BooleanCallbackDelegate callback;

    public override void Awake()
    {
        BUTTON_VERTICAL_MARGIN = ((virtualHeight - 100) - (BUTTON_HEIGHT)) / 2;
        BUTTON_HORIZONTAL_MARGIN = (virtualWidth - (BUTTON_WIDTH * 2)) / 2 - BUTTON_INTERNAL_MARGIN/2;
        base.Awake();
    }

    public IEnumerator ShowButtons(BooleanCallbackDelegate cb)
    {
        this.enabled = true;
        this.callback = cb;
        while (this.enabled)
        {
            yield return 0;
        }
    }

    private void chooseAnswer(bool answer)
    {
        this.enabled = false;
        this.callback(answer);
    }



    public void OnGUI()
    {
        base.OnGUI();
        if (Time.timeScale > 0.0f)
        {
            if (GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN, BUTTON_VERTICAL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT), "Yes"))
            {
                chooseAnswer(true);
            }
            if (GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_INTERNAL_MARGIN + BUTTON_WIDTH, BUTTON_VERTICAL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT), "No"))
            {
                chooseAnswer(false);
            }
        }
    }
}
