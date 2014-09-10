using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public static float moveWaitTime = 0.3f;
    public static int moveSpeed = 20;
   
	public int currentTileNumber;
	public GameController gameController;
	public bool stringShotted = false;
	public int playerNumber;
	public Pokemon pokemon;
	public PokemonController pokeController;
	public CallbackDelegate callback;
    public int legendaryBirds;
	
	private DicerollController roller;
	private int badges = 0;
	
	
	public List<AftereffectController> startOfTurnEffects;
	public Stack<AftereffectController> currentStartOfTurnEffects;
	public List<AftereffectController> beforeTurnEffects;
	public AftereffectController rollReplaceAfterEffect;


    private int extraTurns = 0;
    private int turnsToSkip = 0;
    private string turnSkipMessage;
    private bool hasMagikarp;

    public void getMagikarp() {
        hasMagikarp = true;
    }

    public bool landedOnMagikarp(){
        return hasMagikarp;
    }

    public void getExtraTurns(int turns) {
        extraTurns += turns;
    }

    public void skipTurns(int turns, string message)
    {
        turnsToSkip += turns;
        turnSkipMessage = message;
    }

	void Start(){
        legendaryBirds = 0;
        hasMagikarp = false;
		roller = new DicerollController(gameController);
		currentStartOfTurnEffects = new Stack<AftereffectController>();
		startOfTurnEffects = new List<AftereffectController>();
		beforeTurnEffects = new List<AftereffectController>();
	}
	
	public void setup(GameController controller, int playerNumber, Pokemon pokemon){
		gameController = controller;
        currentTileNumber = gameController.startingTileNumber;
        pokeController = new PokemonController(pokemon, GUIController.Instance, this);
		this.playerNumber = playerNumber;
        this.transform.position = this.gameController.getFreeSpace(currentTileNumber);
	}
	
	public void takeTurn(CallbackDelegate cb){
		callback = cb;
        if (turnsToSkip > 0)
        {
            turnsToSkip--;
            GUIController.Instance.DisplayBasicModal(turnSkipMessage, endTurn);
        }
        else {
            announceTurn();
        }
	}
	
	private void announceTurn(){
        GUIController.Instance.DisplayBasicModal(getAnnounceTurnMessage(), handleStartOfTurnAfterEffects);
	}
	
	public string getAnnounceTurnMessage(){
		return "Player " + playerNumber + "'s Turn.";
	}
	
	public void handleStartOfTurnAfterEffects(){
		foreach(AftereffectController effect in startOfTurnEffects){
			currentStartOfTurnEffects.Push (effect);
		}
		handleNextStartOfTurnEffect();
	}
	
	public void handleNextStartOfTurnEffect(){
		if(currentStartOfTurnEffects.Count == 0){
			roll ();
		} else {
			AftereffectController effect = currentStartOfTurnEffects.Pop();
			effect.applyEffect();
		}
	}
	
	public void roll(){
		if(rollReplaceAfterEffect == null){
			roller.doDiceRoll(move);
		} else {
			rollReplaceAfterEffect.applyEffect();
		}
	}
	
	public void move(int rollResult){
		int spacesToMove = modifyRoll(rollResult);
		if(!gameController.isPassingGoldSquare(currentTileNumber, currentTileNumber + spacesToMove, badges)){
			GoToTile(currentTileNumber + spacesToMove);
		} else {
			GoToTile (gameController.getNextGoldSquare(currentTileNumber));
		}
	}

    public void GoToTile(int tileNum){
        StartCoroutine(goToTile(tileNum));
    }

    private IEnumerator goToTile(int tileNum) {
        if (tileNum > gameController.lastTileNum()) {
            tileNum = gameController.lastTileNum();
        }
        int tilesToMove = Mathf.Abs(currentTileNumber - tileNum);
        int direction = (currentTileNumber - tileNum) < 0 ? 1 : -1;
        for (int i = 0; i < tilesToMove; i ++) {
            currentTileNumber += direction;
            yield return StartCoroutine(animatedMoveToPos(gameController.getFreeSpace(currentTileNumber)));
            yield return new WaitForSeconds(moveWaitTime);
            if (gameController.getCurrentTile() is ImmediateMessageTileController) {
                ImmediateMessageTileController tc = gameController.getCurrentTile() as ImmediateMessageTileController;
                yield return StartCoroutine(tc.showImmediatedMessage());
            }
        }
        gameController.getCurrentTile().applyRules();
    }
    private IEnumerator animatedMoveToPos(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return 0;
        }
    }

    IEnumerator Wait(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
            yield return 0;
    }
	
	public void endTurn(){
		stringShotted = false;

        if (extraTurns > 0)
        {
            extraTurns--;
            announceTurn();
        }
        else {
            callback();
        }
	}
	
	public int modifyRoll(int roll){
		if(stringShotted){
			return (int) Mathf.Ceil(roll/2f);
		}
		return roll;
	}
	
	public void stringShotMe(){
		stringShotted = true;
	}
}
