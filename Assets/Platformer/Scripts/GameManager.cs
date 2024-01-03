using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectGo2D.Platformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public UnityEvent OnGameOver = new UnityEvent();

        // Start is called before the first frame update
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                Instance.OnGameOver.RemoveAllListeners();
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void GameOver()
        {
            OnGameOver.Invoke();
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
            int nextIndex = scene.buildIndex + 1;
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadSceneAsync(nextIndex);
            }
            else
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
    }
}
