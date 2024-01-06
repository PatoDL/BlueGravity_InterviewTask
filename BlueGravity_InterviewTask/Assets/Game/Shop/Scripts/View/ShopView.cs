using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

using BlueGravity.Game.Common.Items.Config;
using BlueGravity.Game.Shop.Items.View;

namespace BlueGravity.Game.Shop.View
{
    public class ShopView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameObject itemViewPrefab = null;
        [SerializeField] private Transform holder = null;
        [SerializeField] private Transform itemsHolder = null;
        [SerializeField] private Button closeButton = null;
        #endregion

        #region PRIVATE_FIELDS
        private ObjectPool<ShopItemView> itemViewPool = null;
        private List<ShopItemView> itemViewList = new List<ShopItemView>();
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

            itemViewPool = new ObjectPool<ShopItemView>(CreateItemView, GetItemView, ReleaseItemView, DestroyItemView);
        }

        public void OpenPanel(List<ItemConfig> itemConfigList)
        {
            ShowItems(itemConfigList);

            holder.gameObject.SetActive(true);
        }

        public void SwitchItemBought(ItemConfig itemConfig, bool bought)
        {
            ShopItemView itemView = itemViewList.Find(iv => iv.Id == itemConfig.Id);

            if (itemView != null)
            {
                itemView.SwitchBought(bought, bought ? trySell : tryBuy);
            }
        }
        #endregion

        #region PRIVATE_METHODS
        private void ClosePanel()
        {
            for (int i = 0; i < itemViewList.Count; i++)
            {
                itemViewPool.Release(itemViewList[i]);
            }

            itemViewList.Clear();

            holder.gameObject.SetActive(false);

            onClosePanel.Invoke();
        }

        private void ShowItems(List<ItemConfig> itemConfigList)
        {
            for (int i = 0; i < itemConfigList.Count; i++)
            {
                ItemConfig itemConfig = itemConfigList[i];
                ShopItemView item = itemViewPool.Get();
                item.Initialize(itemConfig, playerHasItem.Invoke(itemConfig) ? trySell : tryBuy, playerHasItem.Invoke(itemConfig));
                item.transform.SetParent(itemsHolder);
                itemViewList.Add(item);
            }
        }
        #endregion

        #region POOL
        private ShopItemView CreateItemView()
        {
            ShopItemView itemView = Instantiate(itemViewPrefab).GetComponent<ShopItemView>();
            return itemView;
        }

        private void GetItemView(ShopItemView itemView)
        {
            itemView.gameObject.SetActive(true);
        }

        private void ReleaseItemView(ShopItemView itemView)
        {
            itemView.gameObject.SetActive(false);
        }

        private void DestroyItemView(ShopItemView itemView)
        {
            Destroy(itemView.gameObject);
        }
        #endregion
    }
}