using UnityEngine;

using BlueGravity.Game.Common.Camera;
using BlueGravity.Game.Player.Controller;

using BlueGravity.Game.Shop.Controller;
using BlueGravity.Game.Wardrobe.Controller;

namespace BlueGravity.Game.Controller
{
    public class GameController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private ShopController shopController = null;
        [SerializeField] private WardrobeController wardrobeController = null;

        [SerializeField] private CameraZoom cameraZoom = null;
        #endregion

        #region UNITY_CALLS
        private void Start()
        {
            playerController.Initialize(OnPlayerInteracted);

            cameraZoom.Initialize();

            shopController.Initialize(playerController.CanBuyItem, playerController.HasItem,
                    playerController.BuyItem, playerController.SellItem,
                    onClosePanel: () => playerController.InputStatus = true);

            wardrobeController.Initialize(playerController.EquipItem, playerController.UnequipItem, 
                playerController.IsitemEquipped, playerController.GetItems,
                onClosePanel: () =>
                {
                    cameraZoom.ResetZoom();
                    playerController.InputStatus = true;
                });
        }
        #endregion

        #region PRIVATE_METHODS
        private void OnPlayerInteracted(GameObject interactedObject)
        {
            if (interactedObject.TryGetComponent(out ShopController shopController))
            {
                playerController.InputStatus = false;
                shopController.OpenPanel();
            }

            if (interactedObject.TryGetComponent(out WardrobeController wardrobeController))
            {
                playerController.InputStatus = false;
                wardrobeController.OpenPanel();

                cameraZoom.SetZoom(wardrobeController.CameraZoom);
            }
        }
        #endregion
    }
}