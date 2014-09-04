using UnityEngine;
using System.Collections;

public enum GUIState { DISPLAYING_MODAL, DISPLAYING_BUTTON, DISPLAYING_NUMBER_BUTTONS, NONE, DISPLAYING_MODAL_2, SHOWING_DIALOG };

public class GUIController : MonoBehaviour {

    public GUISkin skin;

    public DialogManager dm;

    private Rect ModalWindowPosition;
	private Rect BottomOfScreenButtonPosition = new Rect(50, Screen.height - 50, Screen.width - 100, 50);
	
	private string basicModalText = "";
	private string buttonText = "";
	private CallbackDelegate callback;
	private DicerollCallbackDelegate numberCallback;
	
	private GUIState state = GUIState.NONE;

	private int numberButtonWidth;
	private int numberButtonHeight;
	private int numberButtonVerticalMargin;
	private int numberButtonHorizontalMargin;
    private bool finsihedCoroutine = true;
    private bool hasClicked = true;

	void Awake (){
        setupGuiPositions();
	}

    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            hasClicked = true;
        }
    }

    void setupGuiPositions() {

        ModalWindowPosition = new Rect(0, Screen.height - 100, Screen.width, 100);

	    numberButtonWidth = 100;
	    numberButtonHeight = 100;
	    numberButtonVerticalMargin = (Screen.height - (numberButtonHeight * 2))/2;
	    numberButtonHorizontalMargin = (Screen.width - (numberButtonWidth * 3))/2;
    }

    void OnGUI()
    {
        GUI.skin = skin;
		switch(state){
			case(GUIState.DISPLAYING_MODAL):
				GUI.ModalWindow(1, ModalWindowPosition, drawBasicModal, "");
				break;
            case (GUIState.DISPLAYING_MODAL_2):
                GUI.ModalWindow(1, ModalWindowPosition, drawBasicModal2, "");
                break;
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
	
	public void displayBasicModal(string text, CallbackDelegate cb){
		basicModalText = text;
		callback = cb;
		state = GUIState.DISPLAYING_MODAL;
	}
	
	public void displayBasicButton(string text, CallbackDelegate cb){
		buttonText = text;
		callback = cb;
		state = GUIState.DISPLAYING_BUTTON;
	}

    public IEnumerator displayBasicModal2(string text)
    {

        yield return StartCoroutine(dm.showDialog(text));
    }
	
	public void displaySixNumberButtons(DicerollCallbackDelegate cb){
		numberCallback = cb;
		state = GUIState.DISPLAYING_NUMBER_BUTTONS;
	}
	
	private void finsihed(){
        hasClicked = false;
		state = GUIState.NONE;
        SFXManager.Instance.playBeep();
		callback();
	}

    private void finished2()
    {
        hasClicked = false;
        state = GUIState.NONE;
        SFXManager.Instance.playBeep();
        finsihedCoroutine = true;
    }

	private void drawBasicModal(int windowID){
		GUI.Box (new Rect(0, 0, ModalWindowPosition.width, ModalWindowPosition.height), basicModalText);
		if(hasClicked){
			finsihed();
		}
	}

    private void drawBasicModal2(int windowID)
    {
        GUI.Box(new Rect(0, 0, ModalWindowPosition.width, ModalWindowPosition.height), basicModalText);
        if (hasClicked)
        {
            finished2();
        }
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
