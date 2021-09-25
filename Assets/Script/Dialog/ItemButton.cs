using FlexFramework.Excel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField]
    private Image ItemPicture;
    [SerializeField]
    private Text ItemCount;
    private UserItemData itemInfo;
    [SerializeField]
    private GameObject ItemInfo;

    public void SetInfo(UserItemData itemInfo)
    {
        this.itemInfo = itemInfo;
        Row ItemData = TableDataManager.ItemTool.GetDataById(itemInfo.id);
        ItemPicture.sprite = (Sprite)Resources.Load<Sprite>($"Art/ItemTool/{ItemData[1]}");
        ItemCount.text = itemInfo.count.ToString();
    }

    public void OpenItemInfo()
    {
        ItemInfo.GetComponent<ItemInfo>().SetInfo(this.itemInfo);
    }
}
