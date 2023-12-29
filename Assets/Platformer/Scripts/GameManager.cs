using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectGo2D.Platformer
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        [SerializeField]
        private GameObject gameOverUI;

        // Start is called before the first frame update
        void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
        }

        public void ShowGameOver()
        {
            gameOverUI.SetActive(true);

        }

        // Update is called once per frame
        public void RestartGame()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }

        // Update is called once per frame
        public void NextLevel()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
        }
    }
}
