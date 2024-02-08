using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public class UIButtonList : MonoBehaviour
    {
        public readonly UnityEvent<int> OnActivate = new UnityEvent<int>();
        public readonly UnityEvent<int> OnChangeIndex = new UnityEvent<int>();
        [SerializeField] private List<UIButton> buttons;
        [SerializeField] private int cols;
        [SerializeField] private int rows;
        [SerializeField] private int defaultIndex = 0;
        [SerializeField, ReadOnly] private int currentRow = 1;
        [SerializeField, ReadOnly] private int currentCol = 1;
        [SerializeField, ReadOnly] private int focusedIndex = -1;

        // Start is called before the first frame update
        void Start()
        {
            ResetIndex();
        }

        private void ConfirmHandler()
        {
            buttons[focusedIndex].Activate();
            OnActivate.Invoke(focusedIndex);
        }

        private void MoveHandler(Vector2 inputVector)
        {
            if (inputVector == Vector2.zero) return;
            if (inputVector.x > 0)
            {
                MoveToColumn(currentCol + 1);
            }
            else if (inputVector.x < 0)
            {
                MoveToColumn(currentCol - 1);
            }

            if (inputVector.y > 0)
            {
                MoveToRow(currentRow - 1);
            }
            else if (inputVector.y < 0)
            {
                MoveToRow(currentRow + 1);
            }

            UpdateIndex(GetIndexFromGridPosition());
        }

        private void MoveToColumn(int index)
        {
            currentCol = Mathf.Clamp(index, 1, cols);
        }

        private void MoveToRow(int index)
        {
            currentRow = Mathf.Clamp(index, 1, rows);
        }

        private int GetIndexFromGridPosition()
        {
            int lastRowIndex = currentRow * cols;
            int leftingCols = cols - currentCol;
            return lastRowIndex - leftingCols - 1; ;
        }

        private void UpdateIndex(int newIndex)
        {
            bool hasChanged = newIndex != focusedIndex;
            if (!hasChanged) return;
            if (ExistsButtonAt(focusedIndex)) buttons[focusedIndex].Blur();
            focusedIndex = newIndex;
            if (ExistsButtonAt(focusedIndex)) buttons[focusedIndex].Focus();
            OnChangeIndex.Invoke(focusedIndex);
        }

        private bool ExistsButtonAt(int index)
        {
            return index >= 0 && index < buttons.Count;
        }

        public void ResetIndex()
        {
            if (!ExistsButtonAt(defaultIndex)) return;
            FocusInto(defaultIndex);
        }

        public List<UIButton> GetAllButtons()
        {
            return buttons;
        }

        public int GetFocusedIndex()
        {
            return focusedIndex;
        }

        public void FocusInto(int index)
        {
            UpdateIndex(index);
            currentCol = 1 + index % cols;
            currentRow = 1 + Mathf.CeilToInt(index / cols);
        }

        private void OnEnable()
        {
            InputManager.Instance.OnMoveUICalled.AddListener(MoveHandler);
            InputManager.Instance.OnConfirmUICalled.AddListener(ConfirmHandler);
        }

        private void OnDisable()
        {
            InputManager.Instance.OnMoveUICalled.RemoveListener(MoveHandler);
            InputManager.Instance.OnConfirmUICalled.RemoveListener(ConfirmHandler);
        }

    }
}
