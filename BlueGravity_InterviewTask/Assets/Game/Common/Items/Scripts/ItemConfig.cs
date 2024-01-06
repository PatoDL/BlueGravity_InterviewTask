using UnityEngine;

namespace BlueGravity.Game.Common.Items.Config
{
    [CreateAssetMenu(fileName = "ItemConfig_", menuName = "Game/Shop/Item/ItemConfig", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        #region ENUM
        public enum ITEM_TYPE { HAT, UNDIES, ARMOR, NONE}
        #endregion

        #region EXPOSED_FIELDS
        [SerializeField] private string id = null;
        [SerializeField] private string displayName = null;
        [SerializeField] private Sprite iconDisplaySprite = null;
        [SerializeField] private float iconDisplayScale = 1f;

        [SerializeField] private int price = 0;
        [SerializeField] private ITEM_TYPE itemType = ITEM_TYPE.NONE;
        [SerializeField] private RuntimeAnimatorController animatorController = null;
        #endregion

        #region PROPERTIES
        public string Id { get => id; }
        public string DisplayName { get => displayName; }
        public Sprite IconDisplaySprite { get => iconDisplaySprite; }
        public float IconDisplayScale { get => iconDisplayScale; }

        public int Price { get => price; }
        public ITEM_TYPE ItemType { get => itemType; }
        public RuntimeAnimatorController AnimatorController { get => animatorController; }
        #endregion
    }
}