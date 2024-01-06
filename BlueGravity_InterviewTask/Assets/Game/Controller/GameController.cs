using UnityEngine;

using BlueGravity.Game.Shop.Controller;
using BlueGravity.Game.Player.Controller;

namespace BlueGravity.Game.Controller
{
    public class GameController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerController playerController;
        [SerializeField] private ShopController shopController;
        #endregion

        #region UNITY_CALLS
        private void Start()
        {
            playerController.Initialize(OnPlayerInteracted);
            shopController.Initialize(playerController.CanBuyItem, playerController.HasItem,
                    playerController.BuyItem, playerController.SellItem,
                    onClosePanel: () => playerController.InputStatus = true);
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
        }
        #endregion
    }
}