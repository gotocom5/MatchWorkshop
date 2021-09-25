using FlexFramework.Excel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]
    private Text ItemName;
    [SerializeField]
    private Text Introduce;
    [SerializeField]
    private Image ItemPicture;
    [SerializeField]
    private int itemId = 0;

    public void SetInfo(UserItemData itemInfo)
    {
        if (itemId != itemInfo.id)
        {
            this.itemId = itemInfo.id;
            Row ItemData = TableDataManager.ItemTool.GetDataById(itemInfo.id);
            this.ItemPicture.sprite = (Sprite)Resources.Load<Sprite>($"Art/ItemTool/{ItemData[1]}");
            this.ItemName.text = ItemData[1];
            this.Introduce.text = ItemData[2];
            this.gameObject.SetActive(true);
        }
        else
        {
            itemId = 0;
            this.gameObject.SetActive(false);
        }
    }

    public void UseItemButton()
    {
        DataManager.UserData.UseItem(this.itemId, 1);
        if (DataManager.UserData.GetItemLastCount(this.itemId) == 0) 
        {
            this.gameObject.SetActive(false);
        }
    }
}
