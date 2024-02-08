using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class UITab : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [SerializeField] private GameObject content;

        // Start is called before the first frame update
        void Start()
        {
            button.OnFocus.AddListener(() => content.SetActive(true));
            button.OnBlur.AddListener(() => content.SetActive(false));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
