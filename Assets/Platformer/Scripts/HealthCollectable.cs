using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{

    public class HealthCollectable : MonoBehaviour
    {
        [SerializeField] private float value;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            var health = other.GetComponent<IHealth>();
            health.Heal(value);
            Destroy(gameObject);
        }
    }
}
