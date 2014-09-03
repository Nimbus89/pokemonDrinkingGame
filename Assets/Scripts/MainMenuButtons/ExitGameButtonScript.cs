using UnityEngine;
using System.Collections;

public class ExitGameButtonScript : ButtonScript {

	protected override void DoButtonAction() {
        base.DoButtonAction();
		Application.Quit();
	}
}
