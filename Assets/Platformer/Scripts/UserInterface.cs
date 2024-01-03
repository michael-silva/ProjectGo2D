using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject levelCompleteUI;

        // Start is called before the first frame update
        void Start()
        {
            UpdateScore(0);
            TurnOffGameOver();
            UnpauseGame();
            GameManager.Instance.OnGameOver.AddListener(TurnOnGameOver);
            ScoreManager.Instance.OnScoreUpdated.AddListener(UpdateScore);
            ScoreManager.Instance.OnScoreCompleted.AddListener(CompleteLevel);
        }

        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                PauseGame();
            }
        }

        public void CompleteLevel()
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0;
        }

        private void TurnOnGameOver()
        {
            gameOverUI.SetActive(true);
        }
        private void TurnOffGameOver()
        {
            gameOverUI.SetActive(false);
        }

        public void PauseGame()
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        public void UnpauseGame()
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }

        private void UpdateScore(int score)
        {
            scoreText.text = score.ToString("00") + "/" + ScoreManager.Instance.GetMaxScore().ToString("00");
        }

        public void RestartLevel()
        {
            GameManager.Instance.RestartGame();
            Time.timeScale = 1;
        }

        public void NextLevel()
        {
            GameManager.Instance.NextLevel();
            Time.timeScale = 1;
        }
    }
}
