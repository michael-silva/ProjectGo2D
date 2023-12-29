using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace ProjectGo2D.Platformer
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager instance;
        public static ScoreManager Instance { get { return instance; } }

        [SerializeField]
        private TextMeshProUGUI scoreText;
        private int currentScore;
        private int maxScore;

        public UnityEvent OnScoreCompleted = new UnityEvent();

        // Start is called before the first frame update
        void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void UpdateScore(int points)
        {
            currentScore += points;
            scoreText.text = currentScore.ToString("00") + "/" + maxScore.ToString("00");
            if (IsCompleted())
            {
                OnScoreCompleted.Invoke();
            }
        }

        public void SetMaxScore(int points)
        {
            maxScore = points;
        }
        public bool IsCompleted()
        {
            return maxScore == currentScore;
        }
    }
}
