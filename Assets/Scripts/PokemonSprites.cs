using UnityEngine;
using System.Collections;

public class PokemonSprites {

    private const int POKE_SPRITE_PIXELS_TO_UNITS = 35;

	public static  Texture2D bulbasaurSprite = Resources.Load<Texture2D>("pokemon/1");
	public static  Texture2D ivysaurSprite = Resources.Load<Texture2D>("pokemon/2");
	public static  Texture2D venusaurSprite = Resources.Load<Texture2D>("pokemon/3");
	public static  Texture2D charmanderSprite = Resources.Load<Texture2D>("pokemon/4");
	public static  Texture2D charmeleonSprite = Resources.Load<Texture2D>("pokemon/5");
	public static  Texture2D charizardSprite = Resources.Load<Texture2D>("pokemon/6");
	public static  Texture2D squirtleSprite = Resources.Load<Texture2D>("pokemon/7");
	public static  Texture2D wartortleSprite = Resources.Load<Texture2D>("pokemon/8");
	public static  Texture2D blastoiseSprite = Resources.Load<Texture2D>("pokemon/9");
	public static  Texture2D pikachuSprite = Resources.Load<Texture2D>("pokemon/25");
	public static  Texture2D raichuSprite = Resources.Load<Texture2D>("pokemon/26");
	
	public static Texture2D getTexture(Pokemon pokemon){
		switch(pokemon){
		case Pokemon.bulbasaur:
				return bulbasaurSprite;
			case Pokemon.ivysaur:
				return ivysaurSprite;
			case Pokemon.venusaur:
				return venusaurSprite;
			case Pokemon.charmander:
				return charmanderSprite;
			case Pokemon.charmeleon:
				return charmeleonSprite;
			case Pokemon.charizard:
				return charizardSprite;
			case Pokemon.squirtle:
				return squirtleSprite;
			case Pokemon.wartortle:
				return wartortleSprite;
			case Pokemon.blastoise:
				return blastoiseSprite;
			case Pokemon.pikachu:
				return pikachuSprite;
			case Pokemon.raichu:
				return raichuSprite;
			default:
				return bulbasaurSprite;
		}
	}
	
	public static Sprite getSprite(Pokemon pokemon){
		Texture2D pokeTexture2D = getTexture(pokemon);
        Sprite pokeSprite = Sprite.Create(pokeTexture2D, new Rect(0, 0, pokeTexture2D.width, pokeTexture2D.height), new Vector2(0.5f, 0.5f), POKE_SPRITE_PIXELS_TO_UNITS);
		return pokeSprite;
	}

}
