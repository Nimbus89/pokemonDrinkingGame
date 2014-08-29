using UnityEngine;
using System.Collections;

public class StartGameButtonScript : ButtonScript {

	public int sceneNumber = 1;

	protected override void DoButtonAction() {
		Application.LoadLevel(sceneNumber);
	}
}
