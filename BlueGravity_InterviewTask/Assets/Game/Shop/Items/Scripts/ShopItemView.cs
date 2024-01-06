using System;

using UnityEngine;

using BlueGravity.Game.Common.Items.Config;
using BlueGravity.Game.Common.Items.View;

using TMPro;

namespace BlueGravity.Game.Shop.Items.View
{
    public class ShopItemView : ItemView
    {
        #region EXPOSED_FIELDS
        [SerializeField] private TMP_Text prizeText = null;
        [SerializeField] private GameObject soldPanel = null;

        [SerializeField] private Color buyTextColor = Color.white;
        [SerializeField] private Color sellTextColor = Color.white;
        #endregion

        #region PUBLIC_METHODS
        public void Initialize(ItemConfig itemConfig, Action<string> onButtonClick, bool playerHasItem)
        {
            Initialize(itemConfig);

            prizeText.text = itemConfig.Price.ToString();

            SwitchBought(playerHasItem, onButtonClick);
        }

        public void SwitchBought(bool bought, Action<string> onButtonClick)
        {
            prizeText.color = bought ? sellTextColor : buyTextColor;
            soldPanel.SetActive(bought);

            SetButtonCallback(onButtonClick);
        }
        #endregion
    }
}