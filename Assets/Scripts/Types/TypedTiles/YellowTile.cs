using UnityEngine;
using System.Collections;

public class YellowTile : TypedTileScript
{

    public override void ApplyTypeRules(PokemonType type, CallbackDelegate cb)
    {
        if (type is ElectricType)
        {
            showMessage(cb);
        }
        else
        {
            cb();
        }
    }
}
