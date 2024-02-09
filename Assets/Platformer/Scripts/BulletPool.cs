using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private int poolLimit;
        [SerializeField] private Bullet prefab;
        private List<Bullet> instances = new List<Bullet>();

        // Start is called before the first frame update
        void Awake()
        {
            for (int i = 0; i < poolLimit; i++)
            {
                var instance = Instantiate(prefab, transform.position, transform.rotation, transform);
                instance.gameObject.SetActive(false);
                instances.Add(instance);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Bullet GetBullet(Vector2 position)
        {
            for (int i = 0; i < poolLimit; i++)
            {
                if (instances[i].gameObject.activeSelf) continue;
                instances[i].transform.position = position;
                instances[i].gameObject.SetActive(true);
                return instances[i];
            }
            Debug.LogError("Pool is out of bullets");
            return null;
        }
    }
}
