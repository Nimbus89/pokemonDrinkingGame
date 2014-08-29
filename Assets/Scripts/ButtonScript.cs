using UnityEngine;
using System.Collections;

public abstract class ButtonScript : MonoBehaviour {

	Color startingColor;

	void Start(){
		startingColor = GetComponent<TextMesh>().color;
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
		GetComponent<TextMesh>().color = Color.yellow;
		Debug.Log ("highlight");
	}
	
	protected virtual void Unhighlight(){
		GetComponent<TextMesh>().color = startingColor;
		Debug.Log ("unhighlight");
	}
	
	protected abstract void DoButtonAction();
	
	void Update(){
	
	}
	
}
