using System.Linq;
using System.Collections;
using UnityEngine;

public class DicerollController {
	
	private DicerollCallbackDelegate callback;
    private string currentRandomRollText;
    private string currentRealRollText;
	
	public DicerollController(GameController game){
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
        if (GameController.realMode)
        {
            GUIController.Instance.DisplayDialogThenNumberPicker(currentRealRollText, callback);
		} else {
            GUIController.Instance.DoSingleDiceRoll(currentRandomRollText, callback);
		}
	}

    private void handleDoubleDiceRoll()
    {
        if (GameController.realMode)
        {
            GUIController.Instance.DisplayDialogThenNumberPicker(currentRealRollText, callback);
        }
        else
        {
            GUIController.Instance.DoMultiDiceRoll(currentRandomRollText, (int[] results) => {
                callback(results.Max());
            });
        }
    }

    public void doBattleRoll(string name, DicerollCallbackDelegate cb) {
        currentRandomRollText = string.Format("{0} rolls a \b!", name);
        currentRealRollText = string.Format("What did {0} roll?", name);
        callback = cb;
        handleDiceRoll();
    }

    public void doDoubleBattleRoll(string name, DicerollCallbackDelegate cb)
    {
        currentRandomRollText = string.Format("{0} rolls a \b, then a \b!", name);
        currentRealRollText = string.Format("{0}, roll 2 dice. Enter the higher roll.", name);
        callback = cb;
        handleDoubleDiceRoll();
    }
}
