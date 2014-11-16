using UnityEngine;
using System.Collections;

public class EeveeScript : BasicModalTileController {

    private bool visited;

    public void Start(){
        visited = false;
    }

    protected override string getModalMessage()
    {
        if (!visited)
        {
            visited = true;
            return "You're the first one here. Have an Eevee, and make a rule. Rules must be fair for all players. Rule violations result in a drink.";
        }
        else {
            return "Damn, fresh out of Eevees. No new rule for you! Drink 5 in sadness.";
        }
        
    }
}
