using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectGo2D.Platformer
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        private int currentScore;
        private int maxScore;

        public UnityEvent OnScoreCompleted = new UnityEvent();
        public UnityEvent<int> OnScoreUpdated = new UnityEvent<int>();

        // Start is called before the first frame update
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                Reset();
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Reset()
        {
            Instance.currentScore = 0;
            Instance.OnScoreCompleted.RemoveAllListeners();
            Instance.OnScoreUpdated.RemoveAllListeners();
        }

        public void UpdateScore(int points)
        {
            currentScore += points;
            OnScoreUpdated.Invoke(currentScore);
            if (IsCompleted())
            {
                OnScoreCompleted.Invoke();
            }
        }

        public void SetMaxScore(int points)
        {
            maxScore = points;
        }

        public int GetMaxScore()
        {
            return maxScore;
        }

        public bool IsCompleted()
        {
            return maxScore == currentScore;
        }
    }
}
