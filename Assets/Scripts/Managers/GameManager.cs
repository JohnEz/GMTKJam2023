using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(SceneChanger))]
[RequireComponent(typeof(Quit))]
public class GameManager : Singleton<GameManager> {
    public List<CharacterStats> Adventurers;

    public CharacterStats Player;

    [SerializeField]
    private AudioClip _onDefeatSFX;

    [SerializeField]
    private AudioClip _onVictorySFX;

    public enum GameState {
        Initial,
        Intro,
        PhaseInterlude,
        Combat,
        Victory,
        Defeat,
        MutualDestruction
    }

    public GameState gameState = GameState.Initial;

    public GameObject introDirector;

    public GameObject interludeDirector;

    public bool IsGameActive => gameState == GameState.Combat;

    public bool IsInCutscene =>
        gameState == GameState.Intro
        || gameState == GameState.PhaseInterlude;

    public bool IsGameOver =>
        gameState == GameState.Victory
        || gameState == GameState.Defeat
        || gameState == GameState.MutualDestruction;

    [SerializeField]
    private GameEvent _stopCombatEvent;

    [SerializeField]
    private GameEvent _startCombatEvent;

    public void TransitionGameState(GameState newState) {
        if (newState == gameState) {
            return;
        }

        switch (gameState) {
            case GameState.Combat:
                StopCombat();
                break;
        }

        gameState = newState;

        switch (newState) {
            case GameState.Intro:
                StartIntro();
                break;

            case GameState.Combat:
                StartCombat();
                break;

            case GameState.PhaseInterlude:
                StartInterlude();
                break;

            case GameState.Victory:
                OnGameOver("Defeat...", "(For the intrusive heroes!)", "Replay");
                break;

            case GameState.Defeat:
                OnGameOver("Victory!", "(For the intrusive heroes...)", "Retry");
                break;

            case GameState.MutualDestruction:
                OnGameOver("Mutual Destruction", "Couldn\'t you all just get along?", "Retry");
                break;
        }
    }

    public void StartIntro() {
        introDirector.GetComponent<PlayableDirector>().Play();
        CanvasManager.Instance.CutsceneSkip.OnStartIntro();
    }

    public void EndIntro() {
        CanvasManager.Instance.CutsceneSkip.OnEndIntro();
        TransitionGameState(GameState.Combat);
    }

    public void StartInterlude() {
        interludeDirector.GetComponent<PlayableDirector>().Play();
        CanvasManager.Instance.CutsceneSkip.OnStartInterlude();
    }

    public void EndInterlude() {
        CanvasManager.Instance.CutsceneSkip.OnEndInterlude();
        Player.GetComponentInChildren<Abilities>().EnableAbility(2);
        TransitionGameState(GameState.Combat);
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
        Player.OnDeath += OnPlayerDeath;

        TransitionGameState(GameState.Intro);
    }

    private void OnHealthBarDepleted(int index) {
        if (index == 0) {
            TransitionGameState(GameState.PhaseInterlude);
        }
    }

    private void OnPlayerDeath() {
        // TODO HACK
        Player.GetComponentInChildren<Animator>().SetTrigger("onDeath");
        CheckGameOver();
    }

    private void CheckGameOver() {
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

    private void OnGameOver(string title, string message, string playButtonText) {
        CanvasManager.Instance.GameOverScreen.Show(title, message, playButtonText);
        AudioManager.Instance.GetComponent<AudioSource>().enabled = false;
    }
}
