using UnityEngine;

namespace BlueGravity.Game.Common.Interactable
{
    public interface IInteractable 
    {
        #region PROPERTIES
        public abstract GameObject Sign { get; set; }
        #endregion

        #region PUBLIC_METHODS
        public void OnDetect()
        {
            Sign.SetActive(true);
        }

        public void OnUndetect()
        {
            Sign.SetActive(false);
        }
        #endregion
    }
}
