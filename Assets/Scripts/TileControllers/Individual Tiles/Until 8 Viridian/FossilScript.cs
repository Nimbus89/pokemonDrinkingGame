using UnityEngine;
using System.Collections;

public class FossilScript : BasicModalTileController
{
    protected override string getModalMessage()
    {
        return "You ressurrected a Fossil Pokemon! Everyone older than you drinks 2.";
    }
}
