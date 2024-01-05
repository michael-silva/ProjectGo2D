using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform follow;
        [SerializeField] private float aheadDistance;
        [SerializeField] private float cameraSpeed;
        private float lookAhead;
        private float currentPositionX;
        private Vector3 velocity = Vector3.zero;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed * Time.deltaTime);
            transform.position = new Vector3(follow.position.x + lookAhead, transform.position.y, transform.position.z);
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * follow.localScale.x), speed * Time.deltaTime);

        }

        public void MoveToNextRoom(Transform newRoom)
        {
            currentPositionX = newRoom.transform.position.x;
        }
    }
}
