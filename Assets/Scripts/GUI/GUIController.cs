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
        StartCoroutine(DialogManager.Instance.showDialog(text, true, (int[] results) =>
        {
            cb(results[0]);
        }));
    }

    public IEnumerator DoMultiDiceRoll(string text, MultiDicerollCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text, true, (int[] results) =>
        {
            cb(results);
        }));
    }

    public void DisplaySixNumberButtons(DicerollCallbackDelegate cb)
    {
        StartCoroutine(NumberButtonsManager.Instance.ShowButtons(cb));
    }

    public void DisplayBasicModal(string text, CallbackDelegate cb)
    {
        StartCoroutine(displayBasicModal(text, cb));
    }

    public void DisplayBasicButton(string text, CallbackDelegate cb)
    {
        buttonText = text;
        callback = cb;
        state = GUIState.DISPLAYING_BUTTON;
    }

    public IEnumerator DisplayDialogThenStarterPicker(string text, PokemonCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text, false));
        yield return StartCoroutine(PokemonSelectorManager.Instance.ShowButtons((Pokemon selected) => {
            DialogManager.Instance.enabled = false;
            cb(selected);
        }));
    }

    public void DisplayDialogThenYesNoButtons(string text, BooleanCallbackDelegate cb)
    {
        StartCoroutine(displayDialogThenYesNoButtons(text, cb));
    }
    private IEnumerator displayDialogThenYesNoButtons(string text, BooleanCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text, false));
        yield return StartCoroutine( YesNoButtonManager.Instance.ShowButtons((bool result) =>
        {
            DialogManager.Instance.enabled = false;
            cb(result);
        }));
    }

    public void DisplayDialogThenNumberPicker(string text, DicerollCallbackDelegate cb) {
        StartCoroutine(displayDialogThenNumberPicker(text, cb));
    }
    private IEnumerator displayDialogThenNumberPicker(string text, DicerollCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text, false));
        yield return StartCoroutine(NumberButtonsManager.Instance.ShowButtons((int result) => {
            DialogManager.Instance.enabled = false;
            cb(result);
        }));
    }

    public IEnumerator DisplayBasicDialog(string text)
    {
        return DialogManager.Instance.showDialog(text);
    }

    public void DisplayBasicDialogs(string[] texts, CallbackDelegate cb, bool hideAfter = true)
    {
        StartCoroutine(displayBasicDialogs(texts, cb, hideAfter));
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

    void OnGUI()
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

    private IEnumerator displayBasicDialogs(string[] texts, CallbackDelegate cb, bool hideAfter = true)
    {
        foreach(string text in texts){
            yield return StartCoroutine(DialogManager.Instance.showDialog(text, hideAfter));
        }
        cb();
    }

    private IEnumerator displayBasicModal(string text, CallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text));
        cb();
    }
	
	private void finsihed(){
		state = GUIState.NONE;
        SFXManager.Instance.playBeep();
		callback();
	}		
}
