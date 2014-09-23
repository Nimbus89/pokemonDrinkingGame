using UnityEngine;
using System.Collections;

public class GreenTile : TypedTileScript
{

    public override void ApplyTypeRules(PokemonType type, CallbackDelegate cb)
    {
        if (type is GrassType)
        {
            showMessage(cb);
        }
        else
        {
            cb();
        }
    }
}
