using System;

using UnityEngine;

using BlueGravity.Game.Common.Interactable;

namespace BlueGravity.Game.Common.Detector
{
    public class Detector : MonoBehaviour
    {
        #region ACTIONS
        private Action<GameObject> onTriggerEnter = null;
        private Action<GameObject> onTriggerExit = null;
        #endregion

        #region PUBLIC_REGIONS
        public void Initialize(Action<GameObject> onTriggerEnter, Action<GameObject> onTriggerExit)
        {
            this.onTriggerEnter = onTriggerEnter;
            this.onTriggerExit = onTriggerExit;
        }
        #endregion

        #region UNITY_CALLS
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnDetect();
                onTriggerEnter.Invoke(collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnUndetect();
                onTriggerExit.Invoke(collision.gameObject);
            }
        }
        #endregion
    }
}