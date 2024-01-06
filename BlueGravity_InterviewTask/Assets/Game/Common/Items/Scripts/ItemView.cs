using System;

using UnityEngine;
using UnityEngine.UI;

using BlueGravity.Game.Common.Items.Config;

using TMPro;

namespace BlueGravity.Game.Common.Items.View
{
    public class ItemView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private TMP_Text nameText = null;
        [SerializeField] private Image iconSprite = null;
        [SerializeField] private Button button = null;
        #endregion

        #region PRIVATE_FIELDS
        private string id = null;
        #endregion

        #region PROPERTIES
        public string Id { get => id; }
        #endregion

        #region PUBLIC_METHODS
        public void Initialize(ItemConfig itemConfig)
        {
            id = itemConfig.Id;
            nameText.text = itemConfig.DisplayName;
            iconSprite.sprite = itemConfig.IconDisplaySprite;
            iconSprite.transform.localScale = new Vector3(itemConfig.IconDisplayScale, itemConfig.IconDisplayScale, itemConfig.IconDisplayScale);
        }

        public void SetButtonCallback(Action<string> onButtonClick)
        {
            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(() => onButtonClick.Invoke(id));
        }
        #endregion
    }
}