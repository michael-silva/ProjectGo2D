using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectGo2D.Platformer
{
    public class Respawn : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        private Transform currentCheckPoint;

        // Start is called before the first frame update
        void Start()
        {
            playerHealth = GetComponent<PlayerHealth>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetCheckpoint(Transform checkpoint)
        {
            currentCheckPoint = checkpoint;
        }

        public void Spawn()
        {
            if (currentCheckPoint)
            {
                transform.position = currentCheckPoint.position;
                playerHealth.Respawn();

                // Camera.main.GetComponent<CameraController>().MoveToNextRoom(currentCheckPoint);
            }
            else
            {
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
