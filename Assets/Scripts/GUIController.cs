using UnityEngine;
using System.Collections;

public enum GUIState { DISPLAYING_MODAL, DISPLAYING_BUTTON, DISPLAYING_NUMBER_BUTTONS, NONE, DISPLAYING_MODAL_2 };

public class GUIController : MonoBehaviour {

	private static Rect ModalWindowPosition = new Rect(50, 50, Screen.width - 100, Screen.height - 100);
	private static Rect ModalButton1Position = new Rect(20, 150, 100, 50);
	private GUIStyle ModalTextStyle = new GUIStyle();
	private static Rect StandardButtonPosition = new Rect(50, Screen.height - 50, Screen.width - 100, 50);
	
	private string basicModalText = "";
	private string buttonText = "";
	private CallbackDelegate callback;
	private DicerollCallbackDelegate numberCallback;
	
	private GUIState state = GUIState.NONE;

	private static int NUMBER_BUTTON_WIDTH = 100;
	private static int NUMBER_BUTTON_HEIGHT = 100;
	private static int NUMBER_BUTTON_VERTICAL_MARGIN = (Screen.height - (NUMBER_BUTTON_HEIGHT * 2))/2;
	private static int NUMBER_BUTTON_HORIZONTAL_MARGIN = (Screen.width - (NUMBER_BUTTON_WIDTH * 3))/2;
    private bool finsihedCoroutine = true;

	void Awake (){
		ModalTextStyle.fontSize = 40;
		ModalTextStyle.wordWrap = true;
		ModalTextStyle.normal.textColor = Color.white;
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
				if(GUI.Button(StandardButtonPosition, buttonText)){
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
		if(GUI.Button(new Rect(NUMBER_BUTTON_HORIZONTAL_MARGIN + NUMBER_BUTTON_WIDTH*0, NUMBER_BUTTON_VERTICAL_MARGIN, NUMBER_BUTTON_WIDTH, NUMBER_BUTTON_HEIGHT), "1")){
			state = GUIState.NONE;
			numberCallback(1);
		}
		if(GUI.Button(new Rect(NUMBER_BUTTON_HORIZONTAL_MARGIN + NUMBER_BUTTON_WIDTH*1, NUMBER_BUTTON_VERTICAL_MARGIN, NUMBER_BUTTON_WIDTH, NUMBER_BUTTON_HEIGHT), "2")){
			state = GUIState.NONE;
			numberCallback(2);
		}
		if(GUI.Button(new Rect(NUMBER_BUTTON_HORIZONTAL_MARGIN + NUMBER_BUTTON_WIDTH*2, NUMBER_BUTTON_VERTICAL_MARGIN, NUMBER_BUTTON_WIDTH, NUMBER_BUTTON_HEIGHT), "3")){
			state = GUIState.NONE;
			numberCallback(3);
		}
		if(GUI.Button(new Rect(NUMBER_BUTTON_HORIZONTAL_MARGIN + NUMBER_BUTTON_WIDTH*0, NUMBER_BUTTON_VERTICAL_MARGIN + NUMBER_BUTTON_HEIGHT, NUMBER_BUTTON_WIDTH, NUMBER_BUTTON_HEIGHT), "4")){
			state = GUIState.NONE;
			numberCallback(4);
		}
		if(GUI.Button(new Rect(NUMBER_BUTTON_HORIZONTAL_MARGIN + NUMBER_BUTTON_WIDTH*1, NUMBER_BUTTON_VERTICAL_MARGIN + NUMBER_BUTTON_HEIGHT, NUMBER_BUTTON_WIDTH, NUMBER_BUTTON_HEIGHT), "5")){
			state = GUIState.NONE;
			numberCallback(5);
		}
		if(GUI.Button(new Rect(NUMBER_BUTTON_HORIZONTAL_MARGIN + NUMBER_BUTTON_WIDTH*2, NUMBER_BUTTON_VERTICAL_MARGIN + NUMBER_BUTTON_HEIGHT, NUMBER_BUTTON_WIDTH, NUMBER_BUTTON_HEIGHT), "6")){
			state = GUIState.NONE;
			numberCallback(6);
		}
	}
		
}
