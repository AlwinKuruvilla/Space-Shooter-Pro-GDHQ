using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Menu {
    public class MainMenu : MonoBehaviour {
        public GameObject title;
        public GameObject controlInstructions;
    
        public void LoadNewGame() {
            SceneManager.LoadScene(1);
        }

        public void ShowControls() {
            title.SetActive(false);
            controlInstructions.SetActive(true);
        }
        public void QuitGame() {
            Application.Quit();
        }
    }
}
