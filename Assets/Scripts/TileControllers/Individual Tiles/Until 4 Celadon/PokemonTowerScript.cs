using UnityEngine;
using System.Collections;

public class PokemonTowerScript : ImmediateMessageTileController
{
    protected override string getModalMessage()
    {
        return "Take a drink now for your fallen comrades.";
    }

    public override string getImmediateMessage() {
        return "While in the pokemon tower, you may not speak out of respect for the dead. Take a drink in pennance every time you speak.";
    }
}
