using UnityEngine;
using System.Collections;

public class BlueTile : TypedTileScript
{

    public override void ApplyTypeRules(PokemonType type, CallbackDelegate cb)
    {
        if (type is WaterType)
        {
            showMessage(cb);
        }
        else
        {
            cb();
        }
    }
}
