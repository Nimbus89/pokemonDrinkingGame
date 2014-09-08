using UnityEngine;
using System.Collections;

public abstract class ImmediateMessageTileController : BasicModalTileController {

    public IEnumerator showImmediatedMessage() {
        return GUIController.Instance.DisplayBasicModal(getImmediateMessage());
    }

    public abstract string getImmediateMessage();

}
