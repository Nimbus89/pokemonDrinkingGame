using UnityEngine;
using System.Collections;

public class PokemonController {

	public Sprite sprite;

	private Pokemon pokemon;
	private CallbackDelegate callback;
	private GUIController gui;
	private PlayerController player;
	
	public PokemonController(Pokemon starter, GUIController gui, PlayerController player){
		this.gui = gui;
		this.player = player;
		pokemon = starter;
		refreshSprite();
	}
	
	public void refreshSprite(){
		sprite = PokemonSprites.getSprite(pokemon);
		player.GetComponent<SpriteRenderer>().sprite = sprite;
	}
	
	public void becomePikachi(){
		pokemon = Pokemon.pikachu;
		refreshSprite();
	}
	
	public void Evolve(CallbackDelegate cb){
		string oldPokemonName = Helpers.Titlize(pokemon.ToString());
		callback = cb;
		bool success = true;
		switch(pokemon){
			case Pokemon.bulbasaur:
				pokemon = Pokemon.ivysaur;
				break;
			case Pokemon.ivysaur:
				pokemon = Pokemon.venusaur;
				break;
			case Pokemon.charmander:
				pokemon = Pokemon.charmeleon;
				break;
			case Pokemon.charmeleon:
				pokemon = Pokemon.charizard;
				break;
			case Pokemon.squirtle:
				pokemon = Pokemon.wartortle;
				break;
			case Pokemon.wartortle:
				pokemon = Pokemon.blastoise;
				break;
			case Pokemon.pikachu:
				pokemon = Pokemon.raichu;
				break;
			default:
				success = false;
				break;
		}
		if(success){
			gui.displayBasicModal("Your " + oldPokemonName + " evolved into a " + Helpers.Titlize(pokemon.ToString()) + ".", callback);
		} else {
			gui.displayBasicModal("Your pokemon is fully evolved!", callback);
		}
	}

}
