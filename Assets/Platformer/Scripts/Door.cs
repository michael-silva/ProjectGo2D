using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;

        [SerializeField] private Transform previousRoom;

        [SerializeField] private Transform nextRoom;

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
            if (other.transform.position.x < transform.position.x)
            {
                cameraController.MoveToNextRoom(nextRoom);
            }
            else
            {
                cameraController.MoveToNextRoom(previousRoom);
            }
        }
    }
}
