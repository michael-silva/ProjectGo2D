using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectGo2D.Rpg
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private UIButtonList inventoryButtons;
        [SerializeField] private UIButtonList actionButtons;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemQuantity;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private Image itemImage;
        [SerializeField] private int confirmActionIndex;

        // Start is called before the first frame update
        void Start()
        {
            inventoryButtons.OnActivate.AddListener(HandleInventoryActivation);
            inventoryButtons.OnChangeIndex.AddListener(index => ShowItemDetails(index));
            actionButtons.OnActivate.AddListener(HandleActionActivation);

            RemoveActionsMenu();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void HandleActionActivation(int index)
        {
            if (index == confirmActionIndex)
            {
                int inventoryIndex = inventoryButtons.GetFocusedIndex();
                if (character.TryRemoveFromInventory(inventoryIndex))
                {
                    SyncInventory();
                    ShowItemDetails(inventoryIndex);
                }
            }
            StartCoroutine(DisableActions());
        }

        private void HandleInventoryActivation(int index)
        {
            var slot = character.GetInventoryItemAt(index);
            if (slot == null) return;
            StartCoroutine(EnableActions());
        }

        private void SyncInventory()
        {
            var items = character.GetInventoryItems();
            var buttons = inventoryButtons.GetAllButtons();
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i >= items.Count) buttons[i].DisableImage();
                else buttons[i].EnableImage(items[i].item.GetImage());
            }
        }

        private void ShowItemDetails(int index)
        {
            var slot = character.GetInventoryItemAt(index);
            if (slot == null)
            {
                itemName.text = "";
                itemDescription.text = "";
                itemImage.sprite = null;
                itemImage.enabled = false;
                itemQuantity.text = "";
            }
            else
            {
                itemName.text = slot?.item.GetName();
                itemDescription.text = slot?.item.GetDescription();
                itemImage.sprite = slot?.item.GetImage();
                itemImage.enabled = true;
                itemQuantity.text = slot?.quantity.ToString();
            }
        }

        private IEnumerator EnableActions()
        {
            yield return new WaitForEndOfFrame();
            actionButtons.enabled = true;
            actionButtons.gameObject.SetActive(true);
            inventoryButtons.enabled = false;
            actionButtons.ResetIndex();
        }

        private IEnumerator DisableActions()
        {
            yield return new WaitForEndOfFrame();
            inventoryButtons.enabled = true;
            RemoveActionsMenu();
        }

        private void RemoveActionsMenu()
        {
            actionButtons.enabled = false;
            actionButtons.gameObject.SetActive(false);

        }

        private void OnEnable()
        {
            SyncInventory();
            ShowItemDetails(inventoryButtons.GetFocusedIndex());
        }

        private void OnDisable()
        {
            inventoryButtons.ResetIndex();
            RemoveActionsMenu();
        }
    }
}
