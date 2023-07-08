using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public List<CharacterStats> Adventurers;

    public CharacterStats Player;

    public bool isGameOver = false;

    public enum GameState {Intro, Cutscene, Combat, GameOver};
    public GameState gameState = GameState.Intro;

    public void EndIntro() {
        TransitionGameState(GameState.Combat);
    }

    public void TransitionGameState(GameState newState) {
        switch(newState) {
            case GameState.Intro:
            // Play intro
            break;
            case GameState.Cutscene:
            // Play cutscene
            break;
            case GameState.Combat:
                gameState = GameState.Combat;
            // Allow player and enemy movement
            break;
            case GameState.GameOver:
            // Display end screen
            break;
        }
    }

    public bool IsGameActive() {
        return this.gameState == GameState.Combat;
    }

    private void Awake() {
        Adventurers.ForEach((adventurer) => {
            adventurer.OnDeath += HandleAdventurerDeath;
        });

        Player.OnDeath += HandlePlayerDeath;
    }

    private void HandlePlayerDeath() {
        if (isGameOver) {
            return;
        }

        EndGame(false);
    }

    private void HandleAdventurerDeath() {
        if (isGameOver) {
            return;
        }

        bool allEnemiesDead = !Adventurers.Exists(adventurer => !adventurer.IsDead);

        if (allEnemiesDead) {
            EndGame(true);
        }
    }

    private void EndGame(bool playerWon) {
        if (playerWon) {
            Debug.Log("You Win!");
        } else {
            Debug.Log("You Lose!");
        }
    }
}
