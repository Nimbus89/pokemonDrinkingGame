using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public static float moveWaitTime = 0.3f;
    public static int moveSpeed = 20;

    const string WINNER_MESSAGE = "{0} wins! {1} drinks 2.";
    const string DRAW_MESSAGE = "It's a tie. Both players drink 1.";

	public int currentTileNumber;
	public GameController gameController;
	public bool stringShotted = false;
	public int playerNumber;
	public Pokemon pokemon;
	public PokemonController pokeController;
	public CallbackDelegate callback;
    public int legendaryBirds;
	
	private DicerollController roller;
	
	public List<AftereffectController> startOfTurnEffects;
	public Stack<AftereffectController> currentStartOfTurnEffects;
	public List<AftereffectController> beforeTurnEffects;
	public AftereffectController rollReplaceAfterEffect;


    private int extraTurns = 0;
    private int turnsToSkip = 0;
    private string turnSkipMessage;
    private bool hasMagikarp;
    private string playerName;
    private HashSet<TileController> visitedTiles;

    public string getName() {
        return this.playerName;
    }

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
        visitedTiles = new HashSet<TileController>();
        legendaryBirds = 0;
        hasMagikarp = false;
		roller = new DicerollController(gameController);
		currentStartOfTurnEffects = new Stack<AftereffectController>();
		startOfTurnEffects = new List<AftereffectController>();
		beforeTurnEffects = new List<AftereffectController>();
	}
	
	public void setup(GameController controller, int playerNumber, Pokemon pokemon, string name){
		gameController = controller;
        currentTileNumber = gameController.startingTileNumber;
        pokeController = new PokemonController(pokemon, GUIController.Instance, this);
		this.playerNumber = playerNumber;
        this.transform.position = this.gameController.getFreeSpace(currentTileNumber);
        this.playerName = name;
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
        GUIController.Instance.DisplayBasicModal(getAnnounceTurnMessage(), checkForStartOfTurnEffectTile);
	}

    private void checkForStartOfTurnEffectTile() {
        if (getCurrentTile() is StartOfTurnEffectTileController)
        {
            StartOfTurnEffectTileController controller = getCurrentTile() as StartOfTurnEffectTileController;
            controller.doStartOfTurnRules(this, handleStartOfTurnAfterEffects);
        }
        else 
        {
            handleStartOfTurnAfterEffects();
        }
    }

	public string getAnnounceTurnMessage(){
		return playerName + "'s Turn.";
	}
	
	public void handleStartOfTurnAfterEffects(){
        currentStartOfTurnEffects = new Stack<AftereffectController>();
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
			roller.doNormalDiceRoll(move);
		} else {
			rollReplaceAfterEffect.applyEffect();
		}
	}
	
	public void move(int rollResult){
		int spacesToMove = modifyRoll(rollResult);
		GoToTile(currentTileNumber + spacesToMove);
	}

    public void MoveBack(int spaces, CallbackDelegate cb) {
        StartCoroutine(goToTile(this.currentTileNumber - spaces, false, cb));
    }

    public void MoveTo(int tileNum, CallbackDelegate cb)
    {
        StartCoroutine(goToTile(tileNum, false, cb));
    }

    public void GoToTile(int tileNum){
        StartCoroutine(goToTile(tileNum));
    }

    private IEnumerator goToTile(int tileNum, bool applyRules = true, CallbackDelegate cb = null) {
        if (tileNum > gameController.lastTileNum()) {
            tileNum = gameController.lastTileNum();
        }
        int tilesToMove = Mathf.Abs(currentTileNumber - tileNum);
        int direction = (currentTileNumber - tileNum) < 0 ? 1 : -1;
        for (int i = 0; i < tilesToMove; i ++) {
            currentTileNumber += direction;
            yield return StartCoroutine(animatedMoveToPos(gameController.getFreeSpace(currentTileNumber)));
            yield return new WaitForSeconds(moveWaitTime);
            if (direction == 1)
            {
                if (getCurrentTile() is ImmediateMessageTileController)
                {
                    ImmediateMessageTileController tc = getCurrentTile() as ImmediateMessageTileController;
                    yield return StartCoroutine(tc.showImmediatedMessage());
                }
                if (getCurrentTile().IS_GOLD && !visitedTiles.Contains(getCurrentTile()))
                {
                    break;
                }
            }

        }
        foreach (PlayerController otherPlayer in getCurrentTile().GetPlayersOnMe()) {
            if (otherPlayer != this) {
                yield return StartCoroutine(doBattle(otherPlayer));
            }
        }
        if (applyRules)
        {
            visitedTiles.Add(getCurrentTile());
            getCurrentTile().applyRules(this);
        } else {
            cb();
        }
    }

    private IEnumerator doBattle(PlayerController otherPlayer)
    {
        yield return StartCoroutine(GUIController.Instance.DisplayBasicDialog(
            string.Format("{0} challenges {1} to a trainer battle!", this.playerName, otherPlayer.playerName)));

        if (this.GetPokemonType().IsWeakTo(otherPlayer.GetPokemonType()))
        {
            yield return StartCoroutine(doLopsidedBattle(otherPlayer, this));
        }
        else if (otherPlayer.GetPokemonType().IsWeakTo(this.GetPokemonType())) 
        {
            yield return StartCoroutine(doLopsidedBattle(this, otherPlayer));
        }
        else 
        {
            yield return StartCoroutine(doEvenBattle(this, otherPlayer));
        }
    }

    private IEnumerator doLopsidedBattle(PlayerController strongPlayer, PlayerController weakPlayer) { 
        return doEvenBattle(strongPlayer, weakPlayer);
    }

    private IEnumerator doEvenBattle(PlayerController player1, PlayerController player2)
    {
        bool finished = false;
        roller.doBattleRoll(player1.playerName, (int player1Roll) =>
        {
            roller.doBattleRoll(player2.playerName, (int player2Roll) =>
            {
                string resultMessage;
                if (player1Roll > player2Roll)
                {
                    resultMessage = string.Format(WINNER_MESSAGE, player1.playerName, player2.playerName);
                }
                else if (player2Roll > player1Roll) 
                {
                    resultMessage = string.Format(WINNER_MESSAGE, player2.playerName, player1.playerName);
                }
                else
                {
                    resultMessage = DRAW_MESSAGE;
                }
                GUIController.Instance.DisplayBasicModal(resultMessage, () =>
                {
                    finished = true;
                });
            });
        });
        while (!finished) {
            yield return 0;
        }
    }

    public TileController getCurrentTile() {
        return gameController.GetTileByNum(this.currentTileNumber);
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

    public void EvolveStarter(CallbackDelegate cb) {
        pokeController.Evolve(cb);
    }

    public PokemonType GetPokemonType() {
        return this.pokeController.GetPokemonType();
    }
}
