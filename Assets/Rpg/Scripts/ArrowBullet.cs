using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class ArrowBullet : MonoBehaviour
    {
        [SerializeField] private Transform sprite;
        [SerializeField] private float speed;
        [SerializeField] private float distance;
        [SerializeField, ReadOnly] private Vector2 direction;
        private bool broke;
        private float duration;
        private float timer;
        private Hitbox hitbox;

        void Start()
        {
            duration = distance / speed;
            hitbox = GetComponent<Hitbox>();
            hitbox.OnHit.AddListener(() => Broke());
        }

        // Update is called once per frame
        void Update()
        {
            if (broke) return;
            timer += Time.deltaTime;
            if (timer > duration) Broke();
            else transform.Translate(direction * speed * Time.deltaTime);
        }

        private void Broke()
        {
            broke = true;
            hitbox.enabled = false;
            sprite.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }

        public void Shoot(Vector2 dir)
        {
            broke = false;
            timer = 0;
            if (hitbox)
            {
                hitbox.enabled = true;
            }
            var defaultDirection = new Vector2(0, -1);
            direction = dir == Vector2.zero ? defaultDirection : dir;
            if (direction.x < 0)
            {
                sprite.Rotate(0, 0, 90);
                direction.y = 0;
            }
            else if (direction.x > 0)
            {
                sprite.Rotate(0, 0, -90);
                direction.y = 0;
            }
            else if (direction.y < 0)
            {
                sprite.Rotate(0, 0, 180);
            }
            else if (direction.y > 0)
            {
                sprite.Rotate(0, 0, 0);
            }
        }
    }
}
