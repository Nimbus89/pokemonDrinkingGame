using UnityEngine;
using System.Collections;

public interface StartOfTurnEffectTileController
{
    void doStartOfTurnRules(PlayerController player, CallbackDelegate cb);
}
