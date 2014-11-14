using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogManager : BaseGUIManager<DialogManager>
{

    public static Texture2D downArrowImage;

    private int textLeftMargin = 16;
    private int topLineOffset = 30;
    private int bottomLineOffset = 60;
    private int lineLength = 28;
    private float characterWait = 0.02F;
    private int skipMultiplier = 4;
    private int scrollAnimationOffset = 0;
    private int arrowBlinkSpeed = 40;
    private bool skipping = false;
    private bool showingSkipButton = false;

    string currentTopLine;
    string currentBottomLine;
    string[] currentDialogLines;

    private Rect ModalWindowPosition = new Rect(0, virtualHeight - 100, virtualWidth, 100);
    private Rect SkipButtonPosition = new Rect(virtualWidth - 100, 0, 100, 50);

    private bool pausedForInput = false;
    private bool arrowVisible = false;

    private List<int> rollResults;

    private static char DICEROLL_CHAR = '\b';


    public override void Awake()
    {
        downArrowImage = Resources.Load<Texture2D>("graphics/DownFacingArrow");
        base.Awake();
    }

    public IEnumerator ShowDialogs(string[] texts, bool hideAfter = true, MultiDicerollCallbackDelegate cb = null, bool showSkip = false)
    {
        skipping = false;
        showingSkipButton = showSkip;
        rollResults = new List<int>();
        
        this.enabled = true;
        foreach(string text in texts){
            currentDialogLines = StringUtils.splitIntoLines(text, lineLength);
            if (!skipping)
            {
                clearLines();
                yield return StartCoroutine(animateTopLine(currentDialogLines[0]));
                if (currentDialogLines.Length > 1)
                {
                    yield return StartCoroutine(animateBottomLine(currentDialogLines[1]));
                }
                yield return StartCoroutine(pauseForInput());
                for (int i = 2; i < currentDialogLines.Length; i++)
                {
                    yield return StartCoroutine(shiftLinesUp());
                    yield return StartCoroutine(animateBottomLine(currentDialogLines[i]));
                    yield return StartCoroutine(pauseForInput());
                }
            }
            else 
            {
                if (currentDialogLines.Length > 1)
                {
                    currentBottomLine = currentDialogLines[currentDialogLines.Length - 1];
                    currentTopLine = currentDialogLines[currentDialogLines.Length - 2];
                }
                else
                {
                    currentBottomLine = "";
                    currentTopLine = currentDialogLines[currentDialogLines.Length - 1];
                }
            }
        }
        if (hideAfter)
        {
            this.enabled = false;
        }
        showingSkipButton = false;
        if (cb != null)
        {
            cb(rollResults.ToArray());
        }
    }

    public IEnumerator ShowDialog(string text, bool hideAfter = true, MultiDicerollCallbackDelegate cb = null, bool showSkipButton = false)
    {
        return ShowDialogs(new string[] { text }, hideAfter, cb, showingSkipButton);
    }

    private void clearLines()
    {
        currentTopLine = "";
        currentBottomLine = "";
    }

    private IEnumerator shiftLinesUp()
    {
        scrollAnimationOffset -= 10;
        yield return new WaitForSeconds(characterWait * 2);
        scrollAnimationOffset -= 10;
        yield return new WaitForSeconds(characterWait * 2);
        currentTopLine = currentBottomLine;
        currentBottomLine = "";
        scrollAnimationOffset = 0;
    }

    IEnumerator animateTopLine(string line) {
        return animateLine(line, true);
    }

    IEnumerator animateBottomLine(string line)
    {
        return animateLine(line, false);
    }

    IEnumerator animateLine(string line, bool top)
    {
        int i = 0;
        while (i < line.Length)
        {
            if (line[i] == DICEROLL_CHAR) {
                yield return StartCoroutine(doDiceRoll(top));
            }
            addToLine(top, line[i++]);
            if (!skipping) {
                yield return new WaitForSeconds(getWaitTime());
            }
        }
    }

    void addToLine(bool top, char character) {
        if (top)
        {
            currentTopLine += character;
        }
        else
        {
            currentBottomLine += character;
        }
    }

    void removeLastCharOfLine(bool top)
    {
        if (top)
        {
            currentTopLine = currentTopLine.Remove(currentTopLine.Length - 1);
        }
        else
        {
            currentBottomLine = currentBottomLine.Remove(currentBottomLine.Length - 1);
        }
    }

    IEnumerator doDiceRoll(bool top) {
        int result = Random.Range(1, 7);
        addToLine(top, result.ToString()[0]);
        pausedForInput = true;
        while (pausedForInput) {
            result = Random.Range(1, 7);
            removeLastCharOfLine(top);
            addToLine(top, result.ToString()[0]);
            yield return 0;
        }
        rollResults.Add(result);
        SFXManager.Instance.playBeep();
    }

    IEnumerator pauseForInput()
    {
        if (!skipping) {
            pausedForInput = true;
            var counter = 0;
            arrowVisible = true;
            while (pausedForInput && !skipping)
            {
                if (counter > arrowBlinkSpeed)
                {
                    counter = 0;
                    arrowVisible = !arrowVisible;
                }
                else
                {
                    counter++;
                }
                yield return 0;
            }
            arrowVisible = false;
            if (!skipping) {
                SFXManager.Instance.playBeep();
            }
        }

    }

    void handleClick() {
        if (pausedForInput)
        {
            pausedForInput = false;
        }
    }

    private float getWaitTime() {
        if (Input.GetMouseButton(0))
        {
            return characterWait / skipMultiplier;
        }
        else 
        {
            return characterWait;
        }
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            handleClick();
        }
    }

    override public void OnGUI(){
        GUI.depth = -10;
        base.OnGUI();
        if (Time.timeScale != 0.0f)
        {
            if (showingSkipButton) {
                if (GUI.Button(SkipButtonPosition, "SKIP"))
                {
                    skipping = true;
                }
            }
            //GUI.Window(1, ModalWindowPosition, drawDialogBox, "");
            GUI.Box(ModalWindowPosition, "");
            GUI.Label(new Rect(textLeftMargin + ModalWindowPosition.xMin, ModalWindowPosition.yMin + topLineOffset + scrollAnimationOffset, ModalWindowPosition.width, ModalWindowPosition.height / 2), currentTopLine);
            GUI.Label(new Rect(textLeftMargin + ModalWindowPosition.xMin, ModalWindowPosition.yMin + bottomLineOffset + scrollAnimationOffset, ModalWindowPosition.width, ModalWindowPosition.height / 2), currentBottomLine);
            if (arrowVisible)
            {
                GUI.DrawTexture(new Rect(ModalWindowPosition.width - 50, ModalWindowPosition.yMin + ModalWindowPosition.height - 35, 28, 20), downArrowImage);
            }
        }
        
    }

}
