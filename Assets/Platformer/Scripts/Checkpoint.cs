using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectGo2D.Platformer
{
    public class Checkpoint : MonoBehaviour
    {
        private bool isActivated;

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
            var respawn = other.GetComponent<Respawn>();
            if (respawn != null)
            {
                isActivated = true;
                enabled = false;
                respawn.SetCheckpoint(transform);
            }
        }
    }
}
