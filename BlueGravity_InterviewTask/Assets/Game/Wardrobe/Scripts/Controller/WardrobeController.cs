using System;
using System.Collections.Generic;

using UnityEngine;

using BlueGravity.Game.Common.Interactable;
using BlueGravity.Game.Common.Items.Config;

using BlueGravity.Game.Wardrobe.View;

namespace BlueGravity.Game.Wardrobe.Controller
{
    public class WardrobeController : MonoBehaviour, IInteractable
    {
        #region EXPOSED_FIELDS
        [SerializeField] private WardrobeView wardrobeView = null;

        [SerializeField] private float cameraZoom = 0f;
        #endregion

        #region EXPOSED_FIELDS
        private List<ItemConfig> itemConfigList = null;
        #endregion

        #region ACTIONS
        private Action<ItemConfig> equipItem = null;
        private Action<ItemConfig> unequipItem = null;
        private Func<ItemConfig, bool> isItemEquipped = null;
        private Func<List<ItemConfig>> getPlayerItems = null;
        #endregion

        #region PROPERTIES
        public float CameraZoom { get => cameraZoom; }
        #endregion

        #region PUBLIC_METHODS
        public void Initialize(Action<ItemConfig> equipItem, Action<ItemConfig> unequipItem, 
            Func<ItemConfig, bool> isItemEquipped, Func<List<ItemConfig>> getPlayerItems, Action onClosePanel)
        {
            this.equipItem = equipItem;
            this.unequipItem = unequipItem;
            this.isItemEquipped = isItemEquipped;
            this.getPlayerItems = getPlayerItems;

            wardrobeView.Initialize(OnItemInteracted, onClosePanel);
        }

        public void OpenPanel()
        {
            itemConfigList = getPlayerItems.Invoke();
            wardrobeView.OpenPanel(itemConfigList);
        }
        #endregion

        #region PRIVATE_METHODS
        private void OnItemInteracted(string id)
        {
            ItemConfig itemConfig = itemConfigList.Find(ic => ic.Id == id);

            if(itemConfig != null)
            {
                if (!isItemEquipped.Invoke(itemConfig))
                {
                    equipItem.Invoke(itemConfig);
                }
                else
                {
                    unequipItem.Invoke(itemConfig);
                }
            }
        }
        #endregion
    }
}