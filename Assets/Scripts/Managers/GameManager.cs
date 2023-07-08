using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<CharacterStats> Adventurers;

    public CharacterStats Player;

    public bool isGameOver = false;

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
