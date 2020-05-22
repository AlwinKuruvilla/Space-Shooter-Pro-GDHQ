using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public GameObject gameOverText;
    public GameObject restartText;
    public bool gameOver = false;

    public int score = 0;

	private void Start() {
		scoreText.text = "Score: " + score;
	}

	private void Update() {
		if (gameOver) {
			if (Input.GetKeyDown(KeyCode.R)) {
				gameOver = false;
				SceneManager.LoadScene(1);
			}

			if (Input.GetKeyDown(KeyCode.Escape)) {
				SceneManager.LoadScene(0);
			}
		}
	}

	public void UpdateLives(int currentHealth) {
        if (currentHealth >= 0) 
	        livesImageDisplay.sprite = lives[currentHealth];
    }

    public void UpdateScore() {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void GameOver() {
	    gameOver = true;
	    restartText.SetActive(true);
	    StartCoroutine(GameOverFlicker());
    }
    IEnumerator GameOverFlicker() {
	    while (true) {
		    gameOverText.SetActive(true);
		    yield return new WaitForSeconds(0.5f);
		    gameOverText.SetActive(false);
		    yield return new WaitForSeconds(0.5f);
	    }
    }
}
