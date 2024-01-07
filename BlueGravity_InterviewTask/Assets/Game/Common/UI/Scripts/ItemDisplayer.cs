using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;

using BlueGravity.Game.Common.Items.Config;
using BlueGravity.Game.Common.Items.View;

public class ItemDisplayer : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private GameObject itemViewPrefab = null;
    [SerializeField] private Transform itemsHolder = null;
    #endregion

    #region PRIVATE_FIELDS
    private ObjectPool<ItemView> itemViewPool = null;
    private List<ItemView> itemViewList = new List<ItemView>();
    #endregion

    #region ACTIONS
    private Action<ItemConfig, ItemView> onGetItem = null;
    #endregion

    #region PUBLIC_METHODS
    public void Initialize(Action<ItemConfig, ItemView> onGetItem)
    {
        this.onGetItem = onGetItem;
        itemViewPool = new ObjectPool<ItemView>(CreateItemView, GetItemView, ReleaseItemView, DestroyItemView);
    }

    public void Clear()
    {
        for (int i = 0; i < itemViewList.Count; i++)
        {
            itemViewPool.Release(itemViewList[i]);
        }

        itemViewList.Clear();
    }

    public void ShowItems(List<ItemConfig> itemConfigList)
    {
        for (int i = 0; i < itemConfigList.Count; i++)
        {
            ItemConfig itemConfig = itemConfigList[i];
            ItemView itemView = itemViewPool.Get();

            onGetItem.Invoke(itemConfig, itemView);

            itemView.transform.SetParent(itemsHolder);
            itemView.transform.SetAsLastSibling();

            itemViewList.Add(itemView);
        }
    }

    public ItemView GetItemWithId(string id)
    {
        return itemViewList.Find(iv => iv.Id == id);
    }
    #endregion

    #region POOL
    private ItemView CreateItemView()
    {
        ItemView itemView = Instantiate(itemViewPrefab).GetComponent<ItemView>();
        return itemView;
    }

    private void GetItemView(ItemView itemView)
    {
        itemView.gameObject.SetActive(true);
    }

    private void ReleaseItemView(ItemView itemView)
    {
        itemView.gameObject.SetActive(false);
    }

    private void DestroyItemView(ItemView itemView)
    {
        Destroy(itemView.gameObject);
    }
    #endregion
}
