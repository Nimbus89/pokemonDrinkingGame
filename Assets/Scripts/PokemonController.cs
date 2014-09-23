using UnityEngine;
using System.Collections;

public class PokemonController {

	public Sprite sprite;

	private Pokemon pokemon;
	private CallbackDelegate callback;
	private GUIController gui;
	private PlayerController player;
    private PokemonType type;
	
	public PokemonController(Pokemon starter, GUIController gui, PlayerController player){
        type = GetType(starter);
		this.gui = gui;
		this.player = player;
		pokemon = starter;
		refreshSprite();
	}

    public PokemonType GetPokemonType() {
        return this.type;
    }
	
	public void refreshSprite(){
		sprite = PokemonSprites.getSprite(pokemon);
		player.GetComponent<SpriteRenderer>().sprite = sprite;
	}
	
	public void becomePikachi(){
		pokemon = Pokemon.pikachu;
        type = new ElectricType();
		refreshSprite();
	}
	
	public void Evolve(CallbackDelegate cb){
		string oldPokemonName = Helpers.Titlize(pokemon.ToString());
		callback = cb;
        int level = 0;
		switch(pokemon){
			case Pokemon.bulbasaur:
				pokemon = Pokemon.ivysaur;
                level = 1;
				break;
			case Pokemon.ivysaur:
				pokemon = Pokemon.venusaur;
                level = 2;
				break;
			case Pokemon.charmander:
				pokemon = Pokemon.charmeleon;
                level = 1;
				break;
			case Pokemon.charmeleon:
				pokemon = Pokemon.charizard;
                level = 2;
				break;
			case Pokemon.squirtle:
				pokemon = Pokemon.wartortle;
                level = 1;
				break;
			case Pokemon.wartortle:
				pokemon = Pokemon.blastoise;
                level = 2;
				break;
			case Pokemon.pikachu:
				pokemon = Pokemon.raichu;
                level = 1;
				break;
			default:
                level = 0;
				break;
		}
		if(level == 1)
        {
            gui.DisplayBasicDialogs(new string[]{
                "Your " + oldPokemonName + " evolved into a " + Helpers.Titlize(pokemon.ToString()) + ".",
                "Everybody drinks. Anyone with a starter weak to yours drinks 2."
            }, callback);
		}
        else if (level == 2)
        {
            gui.DisplayBasicDialogs(new string[]{
                "Your " + oldPokemonName + " evolved into a " + Helpers.Titlize(pokemon.ToString()) + ".",
                "Your POKeMON is fully evolved! Everybody drinks twice. Anyone with a starter weak to yours drinks 4."
            }, callback);
        }
        else if (level == 0) 
        {
            gui.DisplayBasicModal("Your POKeMON can't evolve any further. Sadly drink 3.", callback);
        }

        refreshSprite();
	}

    public static PokemonType GetType(Pokemon pokemon) {
        switch (pokemon) { 
            case Pokemon.squirtle:
            case Pokemon.wartortle:
            case Pokemon.blastoise:
                return new WaterType();
            case Pokemon.charmander:
            case Pokemon.charmeleon:
            case Pokemon.charizard:
                return new FireType();
            case Pokemon.bulbasaur:
            case Pokemon.ivysaur:
            case Pokemon.venusaur:
                return new GrassType();
            default:
                return new ElectricType();
        }
    }

}
