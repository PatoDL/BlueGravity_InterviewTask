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
        [SerializeField] private float money = 0f;
        [SerializeField] protected KeyCode interactionKey = KeyCode.None;

        [SerializeField] protected Rigidbody2D rigidBody2d = null;
        [SerializeField] private Detector detector = null;

        [Header("Animations")]
        [SerializeField] private Animator baseAnimator = null;
        [SerializeField] private Animator[] clothesAnimators = null;
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

            UpdateAnimators(movement);
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

        public List<ItemConfig> GetItems()
        {
            return items;
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

            if(IsitemEquipped(itemConfig))
            {
                UnequipItem(itemConfig);
            }
        }

        public void BuyItem(ItemConfig itemConfig)
        {
            items.Add(itemConfig);
            money -= itemConfig.Price;
        }

        public void EquipItem(ItemConfig itemConfig)
        {
            clothesAnimators[(int)itemConfig.ItemType].runtimeAnimatorController = itemConfig.AnimatorController;
        }

        public void UnequipItem(ItemConfig itemConfig)
        {
            clothesAnimators[(int)itemConfig.ItemType].runtimeAnimatorController = null;
        }

        public bool IsitemEquipped(ItemConfig itemConfig)
        {
            return clothesAnimators[(int)itemConfig.ItemType].runtimeAnimatorController == itemConfig.AnimatorController;
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

        private void UpdateAnimators(Vector2 movement)
        {
            UpdateAnimator(baseAnimator, movement);

            for(int i = 0; i < clothesAnimators.Length; i++)
            {
                if (clothesAnimators[i].runtimeAnimatorController != null)
                {
                    UpdateAnimator(clothesAnimators[i], movement);
                }
            }
        }

        private void UpdateAnimator(Animator animator, Vector2 movement)
        {
            bool movingX = Mathf.Abs(movement.x) > .001f;
            bool movingY = Mathf.Abs(movement.y) > .001f;

            animator.SetBool("movingX", movingX && !movingY);
            animator.SetBool("movingY", movingY);
            animator.SetFloat("speedX", movement.x);
            animator.SetFloat("speedY", movement.y);
        }
        #endregion
    }
}