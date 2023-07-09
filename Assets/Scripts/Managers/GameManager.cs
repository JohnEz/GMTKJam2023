using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager> {

    public List<CharacterStats> Adventurers;

    public CharacterStats Player;

    public bool skipIntro = false;

    public enum GameState {
        Intro,
        Cutscene,
        Combat,
        Victory,
        Defeat,
        MutualDestruction
    }

    public GameState gameState = GameState.Intro;

    public GameObject introDirector;

    public bool IsGameActive => gameState == GameState.Combat;

    public bool IsGameOver =>
        gameState == GameState.Victory
        || gameState == GameState.Defeat
        || gameState == GameState.MutualDestruction;

    public void EndIntro() {
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
                // Allow player and enemy movement
                break;
            case GameState.Victory:
                CanvasManager.Instance.ShowGameOver("Game Over", "The world remains at peril...");
                break;
            case GameState.Defeat:
                CanvasManager.Instance.ShowGameOver("Victory!", "The world is safe again, for now...");
                break;
            case GameState.MutualDestruction:
                CanvasManager.Instance.ShowGameOver("Mutual Destruction", "Couldn\'t you all just get along?");
                break;
        }
    }

    private void Awake() {
        Adventurers.ForEach((adventurer) => {
            adventurer.OnDeath += CheckGameOver;
        });

        Player.OnDeath += CheckGameOver;

        if (skipIntro) {
            TransitionGameState(GameState.Combat);
        } else if (introDirector) {
            introDirector.GetComponent<PlayableDirector>().Play();
        }
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
            }
        } else if (allEnemiesDead) {
            TransitionGameState(GameState.Victory);
        }

    }
}
