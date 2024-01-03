using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public interface ICollectable
    {
        void Collect();
    }

    public class Fruit : MonoBehaviour, ICollectable
    {
        [SerializeField]
        private GameObject sprite;

        [SerializeField]
        private GameObject collected;

        private BoxCollider2D boxCollider;

        [SerializeField]
        private float dismissTimer;
        [SerializeField]
        private int points;

        public void Collect()
        {
            boxCollider.enabled = false;
            collected.SetActive(true);
            sprite.SetActive(false);
            ScoreManager.Instance.UpdateScore(points);
            Destroy(gameObject, dismissTimer);
        }

        // Start is called before the first frame update
        void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            collected.SetActive(false);
            sprite.SetActive(true);
        }

        public int GetScorePoints()
        {
            return points;
        }

    }
}
