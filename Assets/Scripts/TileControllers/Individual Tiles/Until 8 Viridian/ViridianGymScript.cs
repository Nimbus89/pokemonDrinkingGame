using UnityEngine;
using System.Collections;

public class ViridianGymScript : BasicModalTileController
{

    public override bool IS_GOLD { get { return true; } }

    protected override string getModalMessage()
    {
        return "First, take a drink. Then, if you're a guy, guys drink 3. If you're a girl, girls drink 3.";
    }
}
