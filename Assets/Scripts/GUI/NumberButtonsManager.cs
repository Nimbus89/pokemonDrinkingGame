using UnityEngine;
using System.Collections;

public class NumberButtonsManager : BaseGUIManager<NumberButtonsManager>
{

    private int numberButtonInternalMargin;
    private int numberButtonWidth;
    private int numberButtonHeight;
    private float numberButtonVerticalMargin;
    private float numberButtonHorizontalMargin;
    private DicerollCallbackDelegate callback;

	private KeyCode[] keyCodes = {
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6
	};

    public override void Awake()
    {
        base.Awake();
        numberButtonInternalMargin = 10;
	    numberButtonWidth = 80;
	    numberButtonHeight = 80;
        numberButtonVerticalMargin = ((virtualHeight - 150) - (numberButtonHeight * 2)) / 2 - numberButtonInternalMargin / 2 + 50;
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
        if (Time.timeScale > 0.0f)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int buttonNumber = (i * 3) + (j + 1);

					KeyCode keyCode = keyCodes[buttonNumber - 1];

                    Rect position = new Rect(numberButtonHorizontalMargin + ((numberButtonWidth + numberButtonInternalMargin) * j),
                        numberButtonVerticalMargin + ((numberButtonHeight + numberButtonInternalMargin) * i), numberButtonWidth, numberButtonHeight);

                    if (GUI.Button(position, "" + buttonNumber))
                    {
                        numberClicked(buttonNumber);
                    }

					if(Input.GetKeyUp(keyCode))
					{
						numberClicked(buttonNumber);
					}
                }
            }
        }
    }
}
