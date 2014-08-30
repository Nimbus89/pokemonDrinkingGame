using UnityEngine;
using System.Collections;

public abstract class ImmediateMessageTileController : BasicModalTileController {

    public IEnumerator showImmediatedMessage() {
        return gui.displayBasicModal2(getImmediateMessage());
    }

    public abstract string getImmediateMessage();

}
