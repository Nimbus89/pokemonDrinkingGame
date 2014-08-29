using UnityEngine;
using System.Collections;

public class PlayerSetupController : MonoBehaviour {

	private static int BUTTON_WIDTH = 100;
	private static int BUTTON_HEIGHT = 100;
	private static int BUTTON_VERTICAL_MARGIN = (Screen.height - (BUTTON_HEIGHT * 2))/2;
	private static int BUTTON_HORIZONTAL_MARGIN = (Screen.width - (BUTTON_WIDTH * 3))/2;
	private Rect TEXT_BOX = new Rect(BUTTON_HORIZONTAL_MARGIN, BUTTON_VERTICAL_MARGIN, 300, 100);
	private int playersCount = 0;
	public static Pokemon[] starters;
	private int pokemonChosen = 0;

	void OnGUI(){
		if(playersCount == 0){
			playerNumberSelectButtons();
		} else if (pokemonChosen < playersCount) {
			displayCurrentPlayerStarterSelect();
		} else {
			startGame();
		}
	}
	
	void playerNumberSelectButtons(){
		//TODO stick in loop
		if(GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_WIDTH*0, BUTTON_VERTICAL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT), "1")){
			selectPlayerNumber(1);
		}
		if(GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_WIDTH*1, BUTTON_VERTICAL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT), "2")){
			selectPlayerNumber(2);
		}
		if(GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_WIDTH*2, BUTTON_VERTICAL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT), "3")){
			selectPlayerNumber(3);
		}
		if(GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_WIDTH*0, BUTTON_VERTICAL_MARGIN + BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT), "4")){
			selectPlayerNumber(4);
		}
		if(GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_WIDTH*1, BUTTON_VERTICAL_MARGIN + BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT), "5")){
			selectPlayerNumber(5);
		}
		if(GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_WIDTH*2, BUTTON_VERTICAL_MARGIN + BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT), "6")){
			selectPlayerNumber(6);
		}
	}
	
	void selectPlayerNumber(int number){
		playersCount = number;
		starters = new Pokemon[number];
	}
	
	void displayCurrentPlayerStarterSelect(){
	
		GUI.Box(TEXT_BOX, "Player " + (pokemonChosen + 1) + ", choose your pokemon!");
	
		if(GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_WIDTH*0, BUTTON_VERTICAL_MARGIN + BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT), PokemonSprites.getTexture(Pokemon.bulbasaur))){
			choosePokemon(Pokemon.bulbasaur);
		}
		if(GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_WIDTH*1, BUTTON_VERTICAL_MARGIN + BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT), PokemonSprites.getTexture(Pokemon.charmander))){
			choosePokemon(Pokemon.charmander);
		}
		if(GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + BUTTON_WIDTH*2, BUTTON_VERTICAL_MARGIN + BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT), PokemonSprites.getTexture(Pokemon.squirtle))){
			choosePokemon(Pokemon.squirtle);
		}
	}
	
	void choosePokemon(Pokemon pokemon){
		starters[pokemonChosen] = pokemon;
		pokemonChosen ++;
	}
	
	void startGame(){
		Application.LoadLevel(2);
	}

}
