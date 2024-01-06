using UnityEngine;

namespace BlueGravity.Game.Common.Items.Config
{
    [CreateAssetMenu(fileName = "ItemConfig_", menuName = "Game/Shop/Item/ItemConfig", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [SerializeField] private string id = null;
        [SerializeField] private Sprite sprite = null;
        [SerializeField] private int price = 0;
        #endregion

        #region PROPERTIES
        public string Id { get => id; }
        public Sprite Sprite { get => sprite; }
        public int Price { get => price; }
        #endregion
    }
}