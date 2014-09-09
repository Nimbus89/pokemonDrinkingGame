using UnityEngine;

public class DicerollController {
	
	private DicerollCallbackDelegate callback;
	private bool realMode;
	
	public DicerollController(GameController game){
		this.realMode = game.realMode;
	}
	
	public void doDiceRollWithMessage(string message, DicerollCallbackDelegate cb){
		callback = cb;
        GUIController.Instance.DisplayBasicModal(message, handleDiceRoll);
	}
	
	public void doDiceRoll(DicerollCallbackDelegate cb){
		callback = cb;
		handleDiceRoll();
	}
	
	private void handleDiceRoll(){
		if(realMode){
            GUIController.Instance.DisplayDialogThenNumberPicker("What did you roll?", callback);
		} else {
            doRandomRoll();
		}
	}

    private void doRandomRoll() {
        GUIController.Instance.DoSingleDiceRoll("You rolled a \b!", callback);
    }
}
