using UnityEngine;
using System.Collections;

public class CreditScript : BaseGUIManager<CreditScript>
{

    public AudioClip creditsMusic;

    const float borderHeightFraction = 0.24f;
    const int numLabels = 4;
    private float borderHeight;
    private float labelHeight;

    private string[] lines;

	// Use this for initialization
	public override void Start () {
        base.Start();
        MusicManager.Instance.PlayInstantly(creditsMusic);
        borderHeight = virtualHeight * borderHeightFraction;
        labelHeight = (virtualHeight - borderHeight * 2) / numLabels;
        StartCoroutine(doCredits());

        lines = new string[] { "", "", "", "" };
	}
	
    public override void Awake()
    {
        base.Awake();
        this.enabled = true;
    }

    void OnGUI() { 
        base.OnGUI();
        for (int i = 0; i < 4; i++) {
            GUI.Label(new Rect(0, borderHeight + labelHeight * i, virtualWidth, labelHeight), lines[i]);
        }
    }

    IEnumerator doCredits() {
        yield return 0;

        lines = new string[] { "POKeMON", "BLACKOUT VERSION STAFF", "", "" };

        yield return new WaitForSeconds(3);

        lines = new string[] { "POKeMON MASTER", "JAMES COTTER", "", "" };

        yield return new WaitForSeconds(3);

        Application.LoadLevel(0);
    }
}
