using UnityEngine;
using System.Collections;

public class DokkanButtonsManager : BaseGUIManager<DokkanButtonsManager>
{

    public static int DOKKAN_MOVES_AMOUNT = 3;

    private int numberButtonInternalMargin;
    private int numberButtonWidth;
    private int numberButtonHeight;
    private float numberButtonVerticalMargin;
    private float numberButtonHorizontalMargin;
    private PlayerController currentPlayer;
    private DicerollCallbackDelegate callback;

    public override void Awake()
    {
        base.Awake();
        numberButtonInternalMargin = 10;
        numberButtonWidth = 80;
        numberButtonHeight = 80;
        numberButtonVerticalMargin = ((virtualHeight - 150) - numberButtonHeight) / 2 + 50;
        numberButtonHorizontalMargin = (virtualWidth - (numberButtonWidth * 3)) / 2 - numberButtonInternalMargin;
    }

    public IEnumerator ShowButtons(PlayerController currentPlayer, DicerollCallbackDelegate cb)
    {
        this.currentPlayer = currentPlayer;
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

    public void OnGUI()
    {
        base.OnGUI();
        if (Time.timeScale > 0.0f)
        {
            for (int j = 0; j < 3; j++)
            {
                int buttonNumber = currentPlayer.getDokkanMove(j);

                Rect position = new Rect(numberButtonHorizontalMargin + ((numberButtonWidth + numberButtonInternalMargin) * j),
                    numberButtonVerticalMargin, numberButtonWidth, numberButtonHeight);

                if (GUI.Button(position, "" + buttonNumber))
                {
                    numberClicked(j);
                }
            }
        }
    }
}
