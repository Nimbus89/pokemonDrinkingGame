using System.Linq;
using System.Collections;
using UnityEngine;

public class DicerollController {
	
	private DicerollCallbackDelegate callback;
    private string currentRandomRollText;
    private string currentRealRollText;
    private string secondRealRollText;
	
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
            GUIController.Instance.DisplayDialogThenNumberPicker(currentRealRollText, (int firstResult) => {
                GUIController.Instance.DisplayDialogThenNumberPicker(secondRealRollText, (int secondResult) =>
                {
                    callback(Mathf.Max(firstResult, secondResult));
                });
            });
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
        currentRealRollText = string.Format("What did {0} roll for die 1?", name);
        secondRealRollText = string.Format("What did {0} roll for die 2?", name);
        callback = cb;
        handleDoubleDiceRoll();
    }
}
