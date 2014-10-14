using UnityEngine;
using System.Collections;

public enum GUIState { DISPLAYING_BUTTON, NONE};

public class GUIController : BaseGUIManager
{

    public static GUIController Instance{
        get { return instance; }
    }

    private static GUIController instance;

    public GUISkin skin;

	private Rect BottomOfScreenButtonPosition = new Rect(50, virtualHeight - 50, virtualWidth - 100, 50);
	
	private string buttonText = "";
	private CallbackDelegate callback;

    private GUIState state;

    public void DoSingleDiceRoll(string text, DicerollCallbackDelegate cb) {
        StartCoroutine(DialogManager.Instance.ShowDialog(text, true, (int[] results) =>
        {
            cb(results[0]);
        }));
    }

    public IEnumerator DoMultiDiceRoll(string text, MultiDicerollCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text, true, (int[] results) =>
        {
            cb(results);
        }));
    }

    public void DisplaySixNumberButtons(DicerollCallbackDelegate cb)
    {
        StartCoroutine(NumberButtonsManager.Instance.ShowButtons(cb));
    }

    public void DisplayDialog(string text, CallbackDelegate cb)
    {
        StartCoroutine(DisplayDialog_CR(text, cb));
    }

    public void DisplayBasicButton(string text, CallbackDelegate cb)
    {
        buttonText = text;
        callback = cb;
        state = GUIState.DISPLAYING_BUTTON;
    }

    public IEnumerator DisplayDialogThenStarterPicker_CR(string text, PokemonCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text, false));
        yield return StartCoroutine(PokemonSelectorManager.Instance.ShowButtons((Pokemon selected) => {
            DialogManager.Instance.enabled = false;
            cb(selected);
        }));
    }

    public void DisplayDialogThenYesNoButtons(string text, BooleanCallbackDelegate cb)
    {
        StartCoroutine(DisplayDialogThenYesNoButtons_CR(text, cb));
    }

    public IEnumerator DisplayDialogThenYesNoButtons_CR(string text, BooleanCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text, false));
        yield return StartCoroutine( YesNoButtonManager.Instance.ShowButtons((bool result) =>
        {
            DialogManager.Instance.enabled = false;
            cb(result);
        }));
    }

    public void DisplayDialogThenNumberPicker(string text, DicerollCallbackDelegate cb) {
        StartCoroutine(DisplayDialogThenNumberPicker_CR(text, cb));
    }

    private IEnumerator DisplayDialogThenNumberPicker_CR(string text, DicerollCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text, false));
        yield return StartCoroutine(NumberButtonsManager.Instance.ShowButtons((int result) => {
            DialogManager.Instance.enabled = false;
            cb(result);
        }));
    }

    public IEnumerator DisplayBasicDialog_CR(string text)
    {
        return DialogManager.Instance.ShowDialog(text);
    }

    public void DisplayBasicSkippableDialogs(string[] texts, CallbackDelegate cb, bool hideAfter = true)
    {
        StartCoroutine(DisplayBasicSkippableDialogs_CR(texts, cb, hideAfter));
    }

    public void DisplayBasicDialogs(string[] texts, CallbackDelegate cb, bool hideAfter = true)
    {
        StartCoroutine(DisplayBasicDialogs_CR(texts, cb, hideAfter));
    }

    public IEnumerator DisplayBasicDialogs_CR(string[] texts, CallbackDelegate cb, bool hideAfter = true)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialogs(texts, hideAfter, null, false));
        cb();
    }

    public IEnumerator DisplayBasicSkippableDialogs_CR(string[] texts, CallbackDelegate cb = null, bool hideAfter = true)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialogs(texts, hideAfter, null, true));
        if (cb != null) {
            cb();
        }
    }

    public IEnumerator DisplayDialog_CR(string text, CallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text));
        cb();
    }
	
	void Awake (){
        state = GUIState.NONE;
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

    public override void OnGUI()
    {
        base.OnGUI();
		switch(state){
			case(GUIState.DISPLAYING_BUTTON):
				if(GUI.Button(BottomOfScreenButtonPosition, buttonText)){
					finsihed();
				}
				break;
			default:
				break;
		}
	}

	private void finsihed(){
		state = GUIState.NONE;
        SFXManager.Instance.playBeep();
		callback();
	}		
}
