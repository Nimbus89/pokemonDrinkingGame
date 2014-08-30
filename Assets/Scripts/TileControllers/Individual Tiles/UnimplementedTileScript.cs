using UnityEngine;
using System.Collections;

public class UnimplementedTileScript : BasicModalTileController {

    protected override string getModalMessage()
    {
        return "This tile isn't done yet.";
    }
}
