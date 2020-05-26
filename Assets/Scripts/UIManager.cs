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
    public Text ammoText;
    public GameObject gameOverText;
    public GameObject restartText;
    public bool gameOver = false;
    public Slider thrustSlider;
    public int score = 0;

    private Player _player;

    private void Start() {
	    _player = GameObject.Find("Player").GetComponent<Player>();
	    if (!_player) {
		    Debug.LogError("UIManager could not find the Player");
	    }
		
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

		ammoText.text = "Ammo: " + _player.ammoNow + "/" + _player._totalAmmo;
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

    public void ChangeThrust(float thrust) {
	    thrustSlider.value = thrust;
    }

    public void SetMaxThrust(float totalThrust) {
	    thrustSlider.maxValue = totalThrust;
    }
}
