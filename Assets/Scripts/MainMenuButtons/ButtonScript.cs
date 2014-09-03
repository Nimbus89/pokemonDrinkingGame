using UnityEngine;
using System.Collections;

public abstract class ButtonScript : MonoBehaviour {

    public SpriteRenderer cursor;

	void Start(){
        Unhighlight();
	}

	void OnMouseEnter () {
		Highlight();
	}
	
	void OnMouseExit () {
		Unhighlight();
	}
	
	void OnMouseUp () {
		DoButtonAction();
	}
	
	protected virtual void Highlight(){
        cursor.enabled = true;
	}
	
	protected virtual void Unhighlight(){
        cursor.enabled = false;
	}

    protected virtual void DoButtonAction() {
        SFXManager.Instance.playBeep();
    }
	
}
