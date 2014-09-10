using UnityEngine;
using System.Collections;

public class YesNoButtonManager : MonoBehaviour
{

    private static YesNoButtonManager instance;

    public GUISkin skin;

    private static int BUTTON_INTERNAL_MARGIN = 30;
    private static int BUTTON_WIDTH = 100;
    private static int BUTTON_HEIGHT = 100;
    private int BUTTON_VERTICAL_MARGIN;
    private int BUTTON_HORIZONTAL_MARGIN;

    private BooleanCallbackDelegate callback;

    public static YesNoButtonManager Instance
    {
        get { return instance; }
    }

    public void Awake()
    {
        BUTTON_VERTICAL_MARGIN = ((Screen.height - 100) - (BUTTON_HEIGHT)) / 2;
        BUTTON_HORIZONTAL_MARGIN = (Screen.width - (BUTTON_WIDTH * 2)) / 2 - BUTTON_INTERNAL_MARGIN/2;
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
        GUI.skin = skin;

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
