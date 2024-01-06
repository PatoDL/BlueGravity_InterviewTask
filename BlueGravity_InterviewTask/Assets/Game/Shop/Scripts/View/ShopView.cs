using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BlueGravity.Game.Common.Items.Config;
using BlueGravity.Game.Shop.Items.View;
using BlueGravity.Game.Common.Items.View;

namespace BlueGravity.Game.Shop.View
{
    public class ShopView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Transform holder = null;
        [SerializeField] private ItemDisplayer itemDisplayer = null;
        [SerializeField] private Button closeButton = null;
        #endregion

        #region ACTIONS
        private Action<string> tryBuy = null;
        private Action<string> trySell = null;
        private Func<ItemConfig, bool> playerHasItem = null;
        private Action onClosePanel = null;
        #endregion

        #region PUBLIC_METHODS
        public void Initialize(Func<ItemConfig, bool> playerHasItem,
            Action<string> tryBuy, Action<string> trySell, Action onClosePanel)
        {
            this.tryBuy = tryBuy;
            this.trySell = trySell;
            this.playerHasItem = playerHasItem;
            this.onClosePanel = onClosePanel;

            closeButton.onClick.AddListener(ClosePanel);

            itemDisplayer.Initialize(OnItemGet);
        }

        public void OpenPanel(List<ItemConfig> itemConfigList)
        {
            itemDisplayer.ShowItems(itemConfigList);
            holder.gameObject.SetActive(true);
        }

        public void SwitchItemBought(ItemConfig itemConfig, bool bought)
        {
            ItemView itemView = itemDisplayer.GetItemWithId(itemConfig.Id);

            if (itemView != null && itemView is ShopItemView shopItemView)
            {
                shopItemView.SwitchBought(bought, bought ? trySell : tryBuy);
            }
        }
        #endregion

        #region PRIVATE_METHODS
        private void ClosePanel()
        {
            itemDisplayer.Clear();

            holder.gameObject.SetActive(false);

            onClosePanel.Invoke();
        }

        private void OnItemGet(ItemConfig itemConfig, ItemView itemView)
        {
            ShopItemView shopItemView = itemView as ShopItemView;

            shopItemView.Initialize(itemConfig, playerHasItem.Invoke(itemConfig) ? trySell : tryBuy, playerHasItem.Invoke(itemConfig));
        }
        #endregion
    }
}