using UnityEngine;
using System.Collections;

public class RedTile : TypedTileScript
{

    public override void ApplyTypeRules(PokemonType type, CallbackDelegate cb)
    {
        if (type is FireType)
        {
            showMessage(cb);
        }
        else {
            cb();
        }
    }
}
