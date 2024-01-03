using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class FruitsManager : MonoBehaviour
    {
        private Fruit[] fruits;

        // Start is called before the first frame update
        void Start()
        {
            fruits = GetComponentsInChildren<Fruit>();
            int total = fruits.Sum(fruit => fruit.GetScorePoints());
            ScoreManager.Instance.SetMaxScore(total);
        }
    }
}
