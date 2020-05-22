using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
