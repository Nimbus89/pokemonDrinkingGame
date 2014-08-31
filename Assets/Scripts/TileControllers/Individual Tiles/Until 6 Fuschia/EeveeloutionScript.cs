using UnityEngine;
using System.Collections;

public class EeveeloutionScript : BasicModalTileController
{
    protected override string getModalMessage()
    {
        return "The first player here creates a new rule. Rules must be fair for all players. Rule violations result in a drink.";
    }
}
