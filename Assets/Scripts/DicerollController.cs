using System.Collections;
using UnityEngine;

public class DicerollController {
	
	private DicerollCallbackDelegate callback;
	private bool realMode;
    private string currentRandomRollText;
    private string currentRealRollText;
	
	public DicerollController(GameController game){
		this.realMode = game.realMode;
	}
	
	public void doNormalDiceRollWithMessage(string message, DicerollCallbackDelegate cb){
        currentRandomRollText = "You rolled a \b!";
        currentRealRollText = "What did you roll?";
		callback = cb;
        GUIController.Instance.DisplayDialog(message, handleDiceRoll);
	}
	
	public void doNormalDiceRoll(DicerollCallbackDelegate cb){
        currentRandomRollText = "You rolled a \b!";
        currentRealRollText = "What did you roll?";
		callback = cb;
		handleDiceRoll();
	}
	
	private void handleDiceRoll(){
		if(realMode){
            GUIController.Instance.DisplayDialogThenNumberPicker(currentRealRollText, callback);
		} else {
            doRandomRoll();
		}
	}

    private void doRandomRoll() {
        GUIController.Instance.DoSingleDiceRoll(currentRandomRollText, callback);
    }

    public void doBattleRoll(string name, DicerollCallbackDelegate cb) {
        currentRandomRollText = string.Format("{0} rolls a \b!", name);
        currentRealRollText = string.Format("What did {0} roll?", name);
        callback = cb;
        handleDiceRoll();
    }
}
