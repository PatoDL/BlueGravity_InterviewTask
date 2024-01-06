using System;
using System.Collections.Generic;

using UnityEngine;

using BlueGravity.Game.Common.Detector;
using BlueGravity.Game.Common.Items.Config;

namespace BlueGravity.Game.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] protected float speed = 0f;
        [SerializeField] protected KeyCode interactionKey = KeyCode.None;
        [SerializeField] protected Rigidbody2D rigidBody2d = null;

        [SerializeField] private Detector detector = null;
        [SerializeField] private float money = 0f;
        #endregion

        #region PRIVATE_FIELDS
        private Vector2 movement = Vector2.zero;
        private GameObject detectedObject = null;
        private List<ItemConfig> items = new List<ItemConfig>();
        #endregion

        #region ACTIONS
        private Action<GameObject> onInteract = null;
        #endregion

        #region PROPERTIES
        public bool InputStatus { get; set; } = true;
        #endregion

        #region UNITY_CALLS
        private void Update()
        {
            if (InputStatus)
            {
                movement.x = Input.GetAxis("Horizontal");
                movement.y = Input.GetAxis("Vertical");

                if (Input.GetKeyDown(interactionKey))
                {
                    Interact();
                }
            }
            else
            {
                movement = Vector2.zero;
            }
        }

        private void FixedUpdate()
        {
            rigidBody2d.MovePosition((Vector2)transform.position + movement * speed * Time.fixedDeltaTime);
        }
        #endregion

        #region PUBLIC_METHODS
        public void Initialize(Action<GameObject> onInteract)
        {
            this.onInteract = onInteract;
            detector.Initialize(OnObjectDetected, OnObjectExited);
        }

        public bool CanBuyItem(int price)
        {
            return price <= money;
        }

        public bool HasItem(ItemConfig itemConfig)
        {
            return items.Contains(itemConfig);
        }

        public void SellItem(ItemConfig itemConfig)
        {
            items.Remove(itemConfig);
            money += itemConfig.Price;
        }

        public void BuyItem(ItemConfig itemConfig)
        {
            items.Add(itemConfig);
            money -= itemConfig.Price;
        }
        #endregion

        #region PRIVATE_METHODS
        private void OnObjectDetected(GameObject detectedObject)
        {
            this.detectedObject = detectedObject;
        }

        private void OnObjectExited(GameObject exitedObject)
        {
            detectedObject = null;
        }

        private void Interact()
        {
            if (detectedObject != null)
            {
                onInteract.Invoke(detectedObject);
            }
        }
        #endregion
    }
}