using UnityEngine;
using System.Collections;

public enum GUIState { DISPLAYING_BUTTON, DISPLAYING_NUMBER_BUTTONS, NONE};

public class GUIController : MonoBehaviour {

    public static GUIController instance;
    public static GUIController Instance
    {
        get { return instance; }
    }

    public GUISkin skin;

	private Rect BottomOfScreenButtonPosition = new Rect(50, Screen.height - 50, Screen.width - 100, 50);
	
	private string buttonText = "";
	private CallbackDelegate callback;
	private DicerollCallbackDelegate numberCallback;

    private GUIState state;

	private int numberButtonWidth;
	private int numberButtonHeight;
	private int numberButtonVerticalMargin;
	private int numberButtonHorizontalMargin;

	void Awake (){
        state = GUIState.NONE;
        setupGuiPositions();
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

    void setupGuiPositions() {

	    numberButtonWidth = 100;
	    numberButtonHeight = 100;
	    numberButtonVerticalMargin = (Screen.height - (numberButtonHeight * 2))/2;
	    numberButtonHorizontalMargin = (Screen.width - (numberButtonWidth * 3))/2;
    }

    void OnGUI()
    {
        GUI.skin = skin;
		switch(state){
			case(GUIState.DISPLAYING_BUTTON):
				if(GUI.Button(BottomOfScreenButtonPosition, buttonText)){
					finsihed();
				}
				break;
			case(GUIState.DISPLAYING_NUMBER_BUTTONS):
				drawNumberButtons();
				break;
			default:
				break;
		}
	}
	
	public void displayBasicButton(string text, CallbackDelegate cb){
		buttonText = text;
		callback = cb;
		state = GUIState.DISPLAYING_BUTTON;
	}

    public IEnumerator DisplayDialogThenStarterPicker(string text, PokemonCallbackDelegate cb) {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text, false));
        yield return StartCoroutine(PokemonSelectorManager.Instance.ShowButtons(cb));
    }

    public IEnumerator DisplayBasicModal(string text)
    {
        return DialogManager.Instance.showDialog(text);
    }

    public void DisplayBasicModals(string[] texts, CallbackDelegate cb)
    {
        StartCoroutine(displayBasicModals(texts, cb));
    }

    private IEnumerator displayBasicModals(string[] texts, CallbackDelegate cb)
    {
        foreach(string text in texts){
            yield return StartCoroutine(DialogManager.Instance.showDialog(text));
        }
        cb();
    }

    public void DisplayBasicModal (string text, CallbackDelegate cb) {
        StartCoroutine(displayBasicModal(text, cb));
    }

    private IEnumerator displayBasicModal(string text, CallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text));
        cb();
    }
	
	public void displaySixNumberButtons(DicerollCallbackDelegate cb){
		numberCallback = cb;
		state = GUIState.DISPLAYING_NUMBER_BUTTONS;
	}
	
	private void finsihed(){
		state = GUIState.NONE;
        SFXManager.Instance.playBeep();
		callback();
	}

    private void finished2()
    {
        state = GUIState.NONE;
        SFXManager.Instance.playBeep();
    }

	private void drawNumberButtons(){
		if(GUI.Button(new Rect(numberButtonHorizontalMargin + numberButtonWidth*0, numberButtonVerticalMargin, numberButtonWidth, numberButtonHeight), "1")){
			state = GUIState.NONE;
			numberCallback(1);
		}
		if(GUI.Button(new Rect(numberButtonHorizontalMargin + numberButtonWidth*1, numberButtonVerticalMargin, numberButtonWidth, numberButtonHeight), "2")){
			state = GUIState.NONE;
			numberCallback(2);
		}
		if(GUI.Button(new Rect(numberButtonHorizontalMargin + numberButtonWidth*2, numberButtonVerticalMargin, numberButtonWidth, numberButtonHeight), "3")){
			state = GUIState.NONE;
			numberCallback(3);
		}
		if(GUI.Button(new Rect(numberButtonHorizontalMargin + numberButtonWidth*0, numberButtonVerticalMargin + numberButtonHeight, numberButtonWidth, numberButtonHeight), "4")){
			state = GUIState.NONE;
			numberCallback(4);
		}
		if(GUI.Button(new Rect(numberButtonHorizontalMargin + numberButtonWidth*1, numberButtonVerticalMargin + numberButtonHeight, numberButtonWidth, numberButtonHeight), "5")){
			state = GUIState.NONE;
			numberCallback(5);
		}
		if(GUI.Button(new Rect(numberButtonHorizontalMargin + numberButtonWidth*2, numberButtonVerticalMargin + numberButtonHeight, numberButtonWidth, numberButtonHeight), "6")){
			state = GUIState.NONE;
			numberCallback(6);
		}
	}
		
}
