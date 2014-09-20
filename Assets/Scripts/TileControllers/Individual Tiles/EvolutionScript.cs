using UnityEngine;
using System.Collections;

public class EvolutionScript: TileController {

    protected override void doRules()
    {
        currentPlayer.EvolveStarter(returnControlToPlayer);
    }
}
