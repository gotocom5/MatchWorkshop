using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class UserDataController
{
    private List<UserItemData> Items = new List<UserItemData>();
    private bool initialization = true;

    /// <summary>
    /// 設置預設狀態
    /// </summary>
    /// <returns></returns>
    public void SetDefaultItemData()
    {
        this.Items = new List<UserItemData>
       {
       new UserItemData() { id = 1010002, count = 7, },
       new UserItemData() { id = 1010003, count = 6, },
       new UserItemData() { id = 1010004, count = 3, },
       new UserItemData() { id = 1010005, count = 8, },
       new UserItemData() { id = 1010006, count = 5, },
       new UserItemData() { id = 1010008, count = 2, },
       };
        EventParam eventParam = new EventParam();
        EventManager.TriggerEvent(EventManager.EventConfig.BagReset.ToString(), eventParam);
    }
    /// <summary>
    /// 增加物品
    /// </summary>
    /// <param name="id"></param>ID
    /// <param name="count"></param>數量
    public void AddItemData(int id, int count)
    {
        var ItemInfo = this.GetItemByID(id);
        if (ItemInfo != null)
        {
            ItemInfo.count = ItemInfo.count + count;
        }
        else
        {
            this.Items.Add(new UserItemData { id = id, count = count });
        }
        EventParam eventParam = new EventParam();
        EventManager.TriggerEvent(EventManager.EventConfig.BagReset.ToString(), eventParam);
    }

    public UserItemData GetItemByID(int Id)
    {
        return Items.FirstOrDefault(t => t.id == Id);
    }

    public int GetItemLastCount(int Id)
    {
        var ItemInfo = this.GetItemData().FirstOrDefault(t => t.id == Id);
        if (ItemInfo != null)
        {
            return ItemInfo.count;
        }
        else
        {
            return 0;
        }
    }

    public void UseItem(int id, int count)
    {
        var ItemInfo = this.GetItemData().FirstOrDefault(t => t.id == id);
        ItemInfo.count = ItemInfo.count - count;
        if (ItemInfo.count <= 0)
        {
            var DeletInfo = this.GetItemData().FirstOrDefault(t => t.id == id);
            this.Items.Remove(DeletInfo);
        }
        EventParam eventParam = new EventParam();
        EventManager.TriggerEvent(EventManager.EventConfig.BagReset.ToString(), eventParam);
    }
    public List<UserItemData> GetItemData()
    {
        if (this.Items.Count == 0 && initialization)
        {
            try
            {

                LoadData();
            }
            catch
            {
                Debug.LogError("ItemData undefind");
                SetDefaultItemData();
                SaveData();
            }
        }
        return this.Items;
    }
    /// <summary>
    /// 讀取檔案
    /// </summary>
    public void LoadData()
    {
        string ItemData = File.ReadAllText(Application.dataPath + "/Resources/JSON/ItemData.txt");
        UserItemData[] ItemsArray = JsonHelper.FromJson<UserItemData>(ItemData);
        if (ItemsArray != null)
        {
            initialization = false;
        }
        this.Items = ItemsArray.ToList();
    }

    public void SaveData()
    {
        UserItemData[] ItemsArray = new UserItemData[Items.Count];
        ItemsArray = Items.ToArray();
        string serializedData = JsonHelper.ToJson(ItemsArray, true);
        File.WriteAllText(Application.dataPath + "/Resources/JSON/ItemData.txt", serializedData);
    }
}

[System.Serializable]
public class UserItemData
{
    public int id;
    public int count;
}
