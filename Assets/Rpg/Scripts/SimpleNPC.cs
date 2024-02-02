using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class SimpleNPC : InteractiveBehaviour
    {
        [SerializeField] private List<string> speaks;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Interact()
        {
            if (!active || !enabled) return;
            WorldUIManager.Instance.ShowDialog(transform.position, speaks);
        }
    }
}
