using UnityEngine;

namespace BlueGravity.Game.Common.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private UnityEngine.Camera cameraComponent = null;
        [SerializeField] private float lerpTime = 0f;
        #endregion

        #region PRIVATE_FIELDS
        private float initialZoom = 0f;
        private float currentZoom = 0f;
        private float targetZoom = 0f;
        private float lerpCurrentTime = 0f;
        #endregion

        #region UNITY_CALLS
        private void Update()
        {
            if (lerpCurrentTime < lerpTime)
            {
                currentZoom = Mathf.Lerp(currentZoom, targetZoom, lerpCurrentTime / lerpTime);
                lerpCurrentTime += Time.deltaTime;
                cameraComponent.orthographicSize = currentZoom;
            }
        }
        #endregion

        #region PUBLIC_METHODS
        public void Initialize()
        {
            initialZoom = cameraComponent.orthographicSize;
            targetZoom = initialZoom;

            currentZoom = initialZoom;
        }

        public void SetZoom(float zoom)
        {
            targetZoom = zoom;
            lerpCurrentTime = 0f;
        }

        public void ResetZoom()
        {
            SetZoom(initialZoom);
        }
        #endregion
    }
}