using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float power;
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;
        [SerializeField, ReadOnly] private Vector2 direction;
        [SerializeField, ReadOnly] private bool hit;
        [SerializeField, ReadOnly] private string originTag;
        [SerializeField] private float maxLifetime;
        [SerializeField, ReadOnly] private float lifetime;
        private BoxCollider2D boxCollider;
        private float powerMultiplier = 1;

        // Start is called before the first frame update
        void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (hit) return;
            lifetime += Time.deltaTime;
            transform.Translate(direction * speed * Time.deltaTime);
            if (lifetime > maxLifetime)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(originTag) || other.CompareTag("Hidden")) return;
            hit = true;
            var health = other.GetComponent<IHealth>();
            if (health != null)
            {
                Debug.Log(other.name + " damaged by " + (power * powerMultiplier));
                health.TakeDamage(power * powerMultiplier);
            }
            boxCollider.enabled = false;
            animator.SetTrigger("Explode");
            StartCoroutine(Deactivate());
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }

        public void Setup(float force, string _originTag)
        {
            powerMultiplier = force;
            transform.localScale = new Vector3(powerMultiplier, powerMultiplier, 1);
            lifetime = 0;
            originTag = _originTag;
            hit = false;
            boxCollider.enabled = true;
        }

        public void Fire(Vector2 _direction)
        {
            direction = _direction;
            if (transform.localScale.x != _direction.x)
            {
                transform.localScale = new Vector3(_direction.x * transform.localScale.x, transform.localScale.y, 1);
            }
        }
    }
}
