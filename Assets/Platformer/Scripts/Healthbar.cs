using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectGo2D.Platformer
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Image totalHealthbar;
        [SerializeField] private Image currentHealthbar;
        private const int maxHearts = 10;

        // Start is called before the first frame update
        void Awake()
        {
            health.OnMaxHealthChange.AddListener((float value) => totalHealthbar.fillAmount = value / maxHearts);
            health.OnHealthChange.AddListener((float value) => currentHealthbar.fillAmount = value / maxHearts);
        }
    }
}
