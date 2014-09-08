using UnityEngine;

public class DicerollController {
	
	private int result;
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
            GUIController.Instance.displaySixNumberButtons(reactToRoll);
		} else {
			displayRollButton();
		}
	}
	
	private void displayRollButton(){
        GUIController.Instance.displayBasicButton("Roll Dice", afterButtonPressed);
	}
	
	private void afterButtonPressed(){
		int result = Random.Range(1, 6);
		reactToRoll(result);
	}
	
	private void reactToRoll(int result){
		this.result = result;
        GUIController.Instance.DisplayBasicModal("Your rolled a " + result + "!", afterMessage);
	}
	
	private void afterMessage(){
		callback(result);
	}

}
