using UnityEngine;
using System.Collections;

public enum GUIState { DISPLAYING_MODAL, DISPLAYING_BUTTON, DISPLAYING_NUMBER_BUTTONS, NONE, DISPLAYING_MODAL_2 };

public class GUIController : MonoBehaviour {

    private Rect ModalWindowPosition;
    private Rect ModalButton1Position;
	private GUIStyle ModalTextStyle = new GUIStyle();
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

	void Awake (){
		ModalTextStyle.fontSize = 40;
		ModalTextStyle.wordWrap = true;
		ModalTextStyle.normal.textColor = Color.white;
        setupGuiPositions();
	}

    void setupGuiPositions() {

        ModalWindowPosition = new Rect(50, 50, Screen.width - 100, Screen.height - 100);
        ModalButton1Position = new Rect(ModalWindowPosition.width - 125, ModalWindowPosition.height - 75, 100, 50);

	    numberButtonWidth = 100;
	    numberButtonHeight = 100;
	    numberButtonVerticalMargin = (Screen.height - (numberButtonHeight * 2))/2;
	    numberButtonHorizontalMargin = (Screen.width - (numberButtonWidth * 3))/2;
    }
	
	void OnGUI(){
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
        basicModalText = text;
        state = GUIState.DISPLAYING_MODAL_2;
        this.finsihedCoroutine = false;
        while (!finsihedCoroutine) {
            yield return 0;
        }
    }
	
	public void displaySixNumberButtons(DicerollCallbackDelegate cb){
		numberCallback = cb;
		state = GUIState.DISPLAYING_NUMBER_BUTTONS;
	}
	
	private void finsihed(){
		state = GUIState.NONE;
		callback();
	}

    private void finished2()
    {
        state = GUIState.NONE;
        finsihedCoroutine = true;
    }

	private void drawBasicModal(int windowID){
		GUI.Box (new Rect(0, 0, ModalWindowPosition.width, ModalWindowPosition.height), basicModalText, ModalTextStyle);
		if(GUI.Button(ModalButton1Position, "OK")){
			finsihed();
		}
	}

    private void drawBasicModal2(int windowID)
    {
        GUI.Box(new Rect(0, 0, ModalWindowPosition.width, ModalWindowPosition.height), basicModalText, ModalTextStyle);
        if (GUI.Button(ModalButton1Position, "OK"))
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
