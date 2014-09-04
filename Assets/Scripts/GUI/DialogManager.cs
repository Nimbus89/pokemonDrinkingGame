using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour {

    public GUISkin skin;
    public int lineLength = 28;
    public float characterWait = 0.03F;
    public int textLeftMargin;
    public int topLineOffset;
    public int bottomLineOffset;

    string currentShownText;
    string currentTopLine;
    string currentBottomLine;
    string[] currentDialogLines;
    int charactersShown;

    private Rect ModalWindowPosition = new Rect(0, Screen.height - 100, Screen.width, 100);

    private bool pausedForInput = false;
    private bool skipping = false;

    public void Awake() {
        this.enabled = false;
    }

    public IEnumerator showDialog(string text)
    {
        this.enabled = true;
        currentDialogLines = StringUtils.splitIntoLines(text, lineLength);
        yield return StartCoroutine(animateTopLine(currentDialogLines[0]));
        yield return StartCoroutine(animateBottomLine(currentDialogLines[1]));
        yield return StartCoroutine(pauseForInput());
        for (int i = 2; i < currentDialogLines.Length; i++) {
            shiftLinesUp();
            yield return StartCoroutine(animateBottomLine(currentDialogLines[i]));
            yield return StartCoroutine(pauseForInput());
        }
        this.enabled = false;
    }

    private void shiftLinesUp()
    {
        currentTopLine = currentBottomLine;
        currentBottomLine = "";
    }

    IEnumerator animateTopLine(string line) {
        int i = 0;
        while (i < line.Length)
        {
            currentTopLine += line[i++];
            if (!skipping)
            {
                yield return new WaitForSeconds(characterWait);
            }
        }
    }

    IEnumerator animateBottomLine(string line)
    {
        int i = 0;
        while (i < line.Length)
        {
            currentBottomLine += line[i++];
            if (!skipping)
            {
                yield return new WaitForSeconds(characterWait);
            }
        }
    }

    IEnumerator pauseForInput()
    {
        pausedForInput = true;
        skipping = false;
        while (pausedForInput) {
            yield return 0;
        }
    }

    void handleClick() {
        if (pausedForInput)
        {
            pausedForInput = false;
        }
        else
        {
            skipping = true;
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
        GUI.ModalWindow(1, ModalWindowPosition, drawDialogBox, "");
    }

    private void drawDialogBox(int windowID)
    {
        GUI.Box(new Rect(textLeftMargin, topLineOffset, ModalWindowPosition.width, ModalWindowPosition.height / 2), currentTopLine);
        GUI.Box(new Rect(textLeftMargin, bottomLineOffset + ModalWindowPosition.height / 2, ModalWindowPosition.width, ModalWindowPosition.height / 2), currentBottomLine);
    }

}
