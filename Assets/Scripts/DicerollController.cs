using UnityEngine;

public class DicerollController {
	
	private int result;
	private GUIController gui;
	private DicerollCallbackDelegate callback;
	private bool realMode;
	
	public DicerollController(GameController game){
		this.gui = game.gui;
		this.realMode = game.realMode;
	}
	
	public void doDiceRollWithMessage(string message, DicerollCallbackDelegate cb){
		callback = cb;
		gui.displayBasicModal(message, handleDiceRoll);
	}
	
	public void doDiceRoll(DicerollCallbackDelegate cb){
		callback = cb;
		handleDiceRoll();
	}
	
	private void handleDiceRoll(){
		if(realMode){
			gui.displaySixNumberButtons(reactToRoll);
		} else {
			displayRollButton();
		}
	}
	
	private void displayRollButton(){
		gui.displayBasicButton("Roll Dice", afterButtonPressed);
	}
	
	private void afterButtonPressed(){
		int result = Random.Range(1, 6);
		reactToRoll(result);
	}
	
	private void reactToRoll(int result){
		this.result = result;
		gui.displayBasicModal("Your rolled a " + result + "!", afterMessage);
	}
	
	private void afterMessage(){
		callback(result);
	}

}
