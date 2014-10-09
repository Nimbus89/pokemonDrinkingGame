using UnityEngine;
using System.Collections;

public class NumberButtonsManager : BaseGUIManager
{

    private int numberButtonInternalMargin;
    private int numberButtonWidth;
    private int numberButtonHeight;
    private float numberButtonVerticalMargin;
    private float numberButtonHorizontalMargin;
    private DicerollCallbackDelegate callback;

    public static NumberButtonsManager Instance
    {
        get { return instance; }
    }

    private static NumberButtonsManager instance;

    public void Awake()
    {
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
        numberButtonInternalMargin = 10;
	    numberButtonWidth = 100;
	    numberButtonHeight = 100;
        numberButtonVerticalMargin = ((virtualHeight-100) - (numberButtonHeight * 2)) / 2 - numberButtonInternalMargin/2;
        numberButtonHorizontalMargin = (virtualWidth - (numberButtonWidth * 3)) / 2 - numberButtonInternalMargin;
    }

    public IEnumerator ShowButtons(DicerollCallbackDelegate cb)
    {
        this.enabled = true;
        this.callback = cb;
        while (this.enabled)
        {
            yield return 0;
        }
    }

    private void numberClicked(int number)
    {
        this.enabled = false;
        this.callback(number);
    }

    public void OnGUI() {
        base.OnGUI();
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 3; j++) {
                int buttonNumber = (i * 3) + (j + 1);
                
                Rect position = new Rect(numberButtonHorizontalMargin + ((numberButtonWidth + numberButtonInternalMargin) * j),
                    numberButtonVerticalMargin + ((numberButtonHeight + numberButtonInternalMargin) * i), numberButtonWidth, numberButtonHeight);

                if (GUI.Button(position, "" + buttonNumber))
                {
                    numberClicked(buttonNumber);
                }
            }
        }
    }
}
