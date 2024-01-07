using System;
using System.Collections.Generic;

using UnityEngine;

using BlueGravity.Game.Common.Interactable;
using BlueGravity.Game.Common.Items.Config;

using BlueGravity.Game.Shop.View;

namespace BlueGravity.Game.Shop.Controller
{
    public class ShopController : MonoBehaviour, IInteractable
    {
        #region EXPOSED_FIELDS
        [SerializeField] private List<ItemConfig> itemConfigList = new List<ItemConfig>();
        [SerializeField] private ShopView shopView = null;
        #endregion

        #region PROPERTIES
        [field: SerializeField] public GameObject Sign { get; set; } = null;
        #endregion

        #region ACTIONS
        private Func<int, bool> validateItemPurchase = null;
        private Func<ItemConfig, bool> playerHasItem = null;
        private Action<ItemConfig> onBuySuccess = null;
        private Action<ItemConfig> onSellSuccess = null;
        #endregion

        #region PUBLIC_METHODS
        public void Initialize(Func<int, bool> validateItemPurchase, Func<ItemConfig, bool> playerHasItem,
            Action<ItemConfig> onBuySuccess, Action<ItemConfig> onSellSuccess, Action onClosePanel)
        {
            this.validateItemPurchase = validateItemPurchase;
            this.playerHasItem = playerHasItem;

            this.onBuySuccess = onBuySuccess;
            this.onSellSuccess = onSellSuccess;

            shopView.Initialize(playerHasItem, TryBuyItem, SellItem, onClosePanel);
        }

        public void OpenPanel()
        {
            shopView.OpenPanel(itemConfigList);
        }
        #endregion

        #region PRIVATE_METHODS
        private void TryBuyItem(string id)
        {
            ItemConfig itemConfig = itemConfigList.Find(ic => ic.Id == id);

            if (itemConfig != null)
            {
                if (validateItemPurchase.Invoke(itemConfig.Price))
                {
                    onBuySuccess.Invoke(itemConfig);
                    shopView.SwitchItemBought(itemConfig, playerHasItem.Invoke(itemConfig));
                }
            }
        }

        private void SellItem(string id)
        {
            ItemConfig itemConfig = itemConfigList.Find(ic => ic.Id == id);

            if (itemConfig != null)
            {
                onSellSuccess.Invoke(itemConfig);
                shopView.SwitchItemBought(itemConfig, playerHasItem.Invoke(itemConfig));
            }
        }
        #endregion
    }
}