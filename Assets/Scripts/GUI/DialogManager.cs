using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour {

    private static DialogManager instance;

    public GUISkin skin;

    public static Texture2D downArrowImage;

    private int textLeftMargin = 16;
    private int topLineOffset = 30;
    private int bottomLineOffset = 60;
    private int lineLength = 28;
    private float characterWait = 0.03F;
    private int skipMultiplier = 4;
    private int scrollAnimationOffset = 0;
    private int arrowBlinkSpeed = 40;

    string currentTopLine;
    string currentBottomLine;
    string[] currentDialogLines;

    private Rect ModalWindowPosition = new Rect(0, Screen.height - 100, Screen.width, 100);

    private bool pausedForInput = false;
    private bool arrowVisible = false;

    private List<int> rollResults;

    private static char DICEROLL_CHAR = '\b';

    public static DialogManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        downArrowImage = Resources.Load<Texture2D>("graphics/DownFacingArrow");
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

    public IEnumerator showDialog(string text, bool hideAfter = true, MultiDicerollCallbackDelegate cb = null)
    {
        rollResults = new List<int>();
        clearLines();
        this.enabled = true;
        currentDialogLines = StringUtils.splitIntoLines(text, lineLength);
        yield return StartCoroutine(animateTopLine(currentDialogLines[0]));
        if (currentDialogLines.Length > 1)
        {
            yield return StartCoroutine(animateBottomLine(currentDialogLines[1]));
        }
        yield return StartCoroutine(pauseForInput());
        for (int i = 2; i < currentDialogLines.Length; i++) {
            yield return StartCoroutine(shiftLinesUp());
            yield return StartCoroutine(animateBottomLine(currentDialogLines[i]));
            yield return StartCoroutine(pauseForInput());
        }
        if (hideAfter)
        {
            this.enabled = false;
        }
        if (cb != null) {
            cb(rollResults.ToArray());
        }
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
            yield return new WaitForSeconds(getWaitTime());
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
            currentBottomLine = currentBottomLine.Remove(currentTopLine.Length - 1);
        }
    }

    IEnumerator doDiceRoll(bool top) {
        int result = Random.Range(1, 6);
        addToLine(top, result.ToString()[0]);
        pausedForInput = true;
        while (pausedForInput) {
            result = Random.Range(1, 6);
            removeLastCharOfLine(top);
            addToLine(top, result.ToString()[0]);
            yield return 0;
        }
        rollResults.Add(result);
        SFXManager.Instance.playBeep();
    }

    IEnumerator pauseForInput()
    {
        pausedForInput = true;
        var counter = 0;
        arrowVisible = true;
        while (pausedForInput) {
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
        SFXManager.Instance.playBeep();
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
        if (Input.GetMouseButtonDown(0))
        {
            handleClick();
        }
    }

    public void OnGUI(){
        GUI.skin = skin;
        GUI.Window(1, ModalWindowPosition, drawDialogBox, "");
    }

    private void drawDialogBox(int windowID)
    {
        GUI.Box(new Rect(textLeftMargin, topLineOffset + scrollAnimationOffset, ModalWindowPosition.width, ModalWindowPosition.height / 2), currentTopLine);
        GUI.Box(new Rect(textLeftMargin, bottomLineOffset + scrollAnimationOffset, ModalWindowPosition.width, ModalWindowPosition.height / 2), currentBottomLine);
        if (arrowVisible) {
            GUI.DrawTexture(new Rect(ModalWindowPosition.width - 50, ModalWindowPosition.height - 35, 28, 20), downArrowImage);
        }
    }

}
