using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(SceneChanger))]
[RequireComponent(typeof(Quit))]
public class GameManager : Singleton<GameManager> {
    public List<CharacterStats> Adventurers;

    public CharacterStats Player;

    public bool skipIntro = false;

    public bool skipInterlude = false;

    [SerializeField]
    private AudioClip _onDefeatSFX;

    [SerializeField]
    private AudioClip _onVictorySFX;

    public enum GameState {
        Intro,
        Cutscene,
        PhaseInterlude,
        Combat,
        Victory,
        Defeat,
        MutualDestruction
    }

    public GameState gameState = GameState.Intro;

    public GameObject introDirector;
    public GameObject interludeDirector;

    public bool IsGameActive => gameState == GameState.Combat;

    public bool IsGameOver =>
        gameState == GameState.Victory
        || gameState == GameState.Defeat
        || gameState == GameState.MutualDestruction;

    [SerializeField]
    private GameEvent _stopCombatEvent;

    [SerializeField]
    private GameEvent _startCombatEvent;

    public void EndIntro() {
        TransitionGameState(GameState.Combat);
    }

    public void EndInterlude() {
        Player.GetComponentInChildren<Abilities>().EnableAbility(2);
        TransitionGameState(GameState.Combat);
    }

    public void TransitionGameState(GameState newState) {
        gameState = newState;

        switch (newState) {
            case GameState.Intro:
                // Play intro
                break;

            case GameState.Cutscene:
                // Play cutscene
                break;

            case GameState.Combat:
                StartCombat();
                break;

            case GameState.PhaseInterlude:
                StopCombat();
                interludeDirector.GetComponent<PlayableDirector>().Play();
                break;

            case GameState.Victory:
                StopCombat();
                CanvasManager.Instance.GameOverScreen.Show("Game Over", "(For the intrusive heroes.)", "Replay");
                break;

            case GameState.Defeat:
                StopCombat();
                CanvasManager.Instance.GameOverScreen.Show("Victory!", "(For the intrusive heroes.)", "Retry");
                break;

            case GameState.MutualDestruction:
                StopCombat();
                CanvasManager.Instance.GameOverScreen.Show("Mutual Destruction", "Couldn\'t you all just get along?", "Retry");
                break;
        }
    }

    private void StopCombat() {
        if (_stopCombatEvent) {
            _stopCombatEvent.Raise();
        }
    }

    private void StartCombat() {
        if (_startCombatEvent) {
            _startCombatEvent.Raise();
        }
    }

    private void Awake() {
        Adventurers.ForEach((adventurer) => {
            adventurer.OnDeath += CheckGameOver;
        });

        Player.OnHealthBarDepleted += OnHealthBarDepleted;
        Player.OnDeath += CheckGameOver;

        if (skipIntro) {
            TransitionGameState(GameState.Combat);
        } else if (introDirector) {
            introDirector.GetComponent<PlayableDirector>().Play();
        }
    }

    private void OnHealthBarDepleted(int index) {
        if (index == 0) {
            if (skipInterlude) {
                EndInterlude();
            } else {
                TransitionGameState(GameState.PhaseInterlude);
            }
        }
    }

    private void CheckGameOver() {
        // TODO HACK
        Player.GetComponentInChildren<Animator>().SetTrigger("onDeath");

        if (IsGameOver) {
            return;
        }

        bool playerDead = Player.IsDead;
        bool allEnemiesDead = !Adventurers.Exists(adventurer => !adventurer.IsDead);

        if (playerDead) {
            if (allEnemiesDead) {
                TransitionGameState(GameState.MutualDestruction);
            } else {
                TransitionGameState(GameState.Defeat);

                AudioManager.Instance.PlaySound(_onDefeatSFX, transform.position);
            }
        } else if (allEnemiesDead) {
            TransitionGameState(GameState.Victory);

            AudioManager.Instance.PlaySound(_onVictorySFX, transform.position);
        }
    }
}
