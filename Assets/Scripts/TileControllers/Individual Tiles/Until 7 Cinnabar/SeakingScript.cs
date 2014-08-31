using UnityEngine;
using System.Collections;

public class SeakingScript : BasicModalTileController
{
    protected override string getModalMessage()
    {
        return "Seaking used Waterfall! Do a waterfall! (Start drinking after the person on your right starts drinking. You can't stop until the person on your right stops.)";
    }
}
