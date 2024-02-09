using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField, ReadOnly] private bool isPaused;
        [SerializeField, ReadOnly] private bool isVisibleMenu;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            InputManager.Instance.OnPauseCalled.AddListener(TogglePauseGame);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private bool CanTogglePauseMenu()
        {
            return !isPaused || isVisibleMenu;
        }

        private void TogglePauseGame()
        {
            if (!CanTogglePauseMenu()) return;
            isVisibleMenu = !isVisibleMenu;
            if (isVisibleMenu)
            {
                PauseGame();
                InputManager.Instance.EnableUIInputMode();
                UIManager.Instance.ShowInventory();
            }
            else
            {
                ResumeGame();
                InputManager.Instance.EnablePlayerInputMode();
                UIManager.Instance.HideInventory();
            }
        }

        public void PauseGame()
        {
            isPaused = true;
        }
        public void ResumeGame()
        {
            isPaused = false;
        }
        public bool IsPaused()
        {
            return isPaused;
        }
    }
}
