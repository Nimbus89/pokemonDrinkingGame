using UnityEngine;
using System.Collections;

public class DittoScript : BasicModalTileController
{
    protected override string getModalMessage()
    {
        return "Ditto used transform! During the next player's turn, you must copy everything they do!";
    }
}
