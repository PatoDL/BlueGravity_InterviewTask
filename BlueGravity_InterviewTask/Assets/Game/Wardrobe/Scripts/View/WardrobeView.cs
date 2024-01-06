using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BlueGravity.Game.Common.Items.Config;
using BlueGravity.Game.Common.Items.View;

namespace BlueGravity.Game.Wardrobe.View
{
    public class WardrobeView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Transform holder = null;
        [SerializeField] private ItemDisplayer itemDisplayer = null;
        [SerializeField] private Button closeButton = null;
        #endregion

        #region ACTIONS
        private Action<string> onItemInteracted = null;
        private Action onClosePanel = null;
        #endregion

        #region PUBLIC_METHODS
        public void Initialize(Action<string> onItemInteracted, Action onClosePanel)
        {
            this.onItemInteracted = onItemInteracted;
            this.onClosePanel = onClosePanel;

            closeButton.onClick.AddListener(ClosePanel);
            itemDisplayer.Initialize(OnItemGet);
        }

        public void OpenPanel(List<ItemConfig> itemConfigList)
        {
            itemDisplayer.ShowItems(itemConfigList);
            holder.gameObject.SetActive(true);
        }
        #endregion

        #region PRIVATE_METHODS
        private void ClosePanel()
        {
            holder.gameObject.SetActive(false);
            itemDisplayer.Clear();

            onClosePanel.Invoke();
        }

        private void OnItemGet(ItemConfig itemConfig, ItemView itemView)
        {
            itemView.Initialize(itemConfig);
            itemView.SetButtonCallback(onItemInteracted);
        }
        #endregion
    }
}