using UnityEngine;

using TMPro;

namespace BlueGravity.Game.Player.InfoCanvas
{
    public class PlayerInfoCanvas : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private TMP_Text playerMoneyText = null;
        #endregion

        #region PUBLIC_METHODS
        public void UpdateMoneyText(float moneyValue)
        {
            playerMoneyText.text = moneyValue.ToString();
        }
        #endregion
    }
}