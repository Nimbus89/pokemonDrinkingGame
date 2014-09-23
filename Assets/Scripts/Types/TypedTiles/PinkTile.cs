using UnityEngine;
using System.Collections;

public class PinkTile : TypedTileScript {

    public override void ApplyTypeRules(PokemonType type, CallbackDelegate cb)
    {
        showMessage(cb);
    }
}
