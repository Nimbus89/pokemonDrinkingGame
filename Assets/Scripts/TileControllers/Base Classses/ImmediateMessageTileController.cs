using UnityEngine;
using System.Collections;

public abstract class ImmediateMessageTileController : BasicModalTileController {

    public IEnumerator showImmediatedMessage() {
        return GUIController.Instance.DisplayBasicDialog(getImmediateMessage());
    }

    public abstract string getImmediateMessage();

}
