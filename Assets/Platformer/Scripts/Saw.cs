using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Saw : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool isOn;
        [SerializeField] private float movingDuration;
        [SerializeField] private Vector2Int direction;
        private float movingTimer;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!isOn) return;
            movingTimer += Time.deltaTime;
            if (movingTimer > movingDuration)
            {
                movingTimer = 0;
                direction *= -1;
            }
            transform.Translate(new Vector2(direction.x, direction.y) * speed * Time.deltaTime);
        }
    }
}
