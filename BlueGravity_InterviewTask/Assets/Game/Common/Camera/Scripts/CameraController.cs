using UnityEngine;

namespace BlueGravity.Game.Common.Camera
{
    public class CameraController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Transform target = null;
        [SerializeField] private Vector3 targetOffset = Vector3.zero;
        #endregion

        #region UNITY_CALLS
        private void Update()
        {
            transform.position = target.position + targetOffset;
        }
        #endregion
    }
}