using UnityEngine;
using System.Collections;

public class CreditScript : BaseGUIManager<CreditScript>
{

    public AudioClip creditsMusic;

    const float BORDER_HEIGHT_FRACTION = 0.24f;
    const int NUMBER_OF_LABELS = 4;
    const float FADE_SPEED = 0.05f;
    const float FADE_STEPS = 10f;

    private float borderHeight;
    private float labelHeight;

    private string[] lines;

    private GUIStyle labelStyle;

    private string[][] pages = new string[][] {
        new string[] { "POKeMON", "BLACKOUT VERSION STAFF", "", "" },
        new string[] { "LEAD DEVELOPER", "PAUL BROUGHTON", "", "" },
        new string[] { "RULES DEVELOPMENT", "PAUL BROUGHTON", "JAMES COTTER", "" },
        new string[] { "LEAD WRITER", "JAMES COTTER", "", "" },
        new string[] { "BOARD DESIGN", "TREMBLEHORN", "", "" },
        new string[] { "LEAD TESTER", "JAMES COTTER", "", "" },
        new string[] { "TESTING", "JAMES KING", "NALA MURPHY", "JOANN KELLEHER" },
        new string[] { "TESTING", "ROISIN MORAN", "PAUL BROUGHTON", "MARC DERHAM" },
        new string[] { "ORIGINAL DESIGN", "TITAN413", "RAITH112358", "" },
        new string[] { "ACE PROTOEGE", "NALA MURPHY", "", "" },
        new string[] { "SPECIAL THANKS", "GAME FREAK", "", "" },
        new string[] { "POKeMON MASTER", "JAMES COTTER", "", "" },
    };

	// Use this for initialization
	public override void Start () {
        base.Start();
        MusicManager.Instance.PlayInstantly(creditsMusic);
        borderHeight = virtualHeight * BORDER_HEIGHT_FRACTION;
        labelHeight = (virtualHeight - borderHeight * 2) / NUMBER_OF_LABELS;
        labelStyle = new GUIStyle(DEFAULT_SKIN.label);
        labelStyle.normal.textColor = new Color(0, 0, 0, 0);
        labelStyle.alignment = TextAnchor.MiddleCenter;
        StartCoroutine(doCredits());
	}
	
    public override void Awake()
    {
        base.Awake();
        this.enabled = true;
    }

    public override void OnGUI()
    { 
        base.OnGUI();
        for (int i = 0; i < 4; i++) {
            GUI.Label(new Rect(0, borderHeight + labelHeight * i, virtualWidth, labelHeight), lines[i], labelStyle);
        }
    }

    IEnumerator doCredits() {
        foreach (string[] pageLines in pages) {
            lines = pageLines;
            yield return StartCoroutine(fadeIn());
            yield return new WaitForSeconds(4);
            yield return StartCoroutine(fadeOut());
            yield return new WaitForSeconds(1.5f);
        }
        Application.LoadLevel(0);
    }

    IEnumerator fadeOut()
    {
        for (int i = 0; i < FADE_STEPS; i++)
        {
            labelStyle.normal.textColor = new Color(0, 0, 0, labelStyle.normal.textColor.a - (1f/FADE_STEPS));
            yield return new WaitForSeconds(FADE_SPEED);
        }
    }

    IEnumerator fadeIn()
    {
        for (int i = 0; i < FADE_STEPS; i++)
        {
            labelStyle.normal.textColor += new Color(0, 0, 0, (1f/FADE_STEPS));
            yield return new WaitForSeconds(FADE_SPEED);
        }
    }
}
