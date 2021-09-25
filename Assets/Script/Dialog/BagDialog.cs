using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagDialog : MonoBehaviour
{
    [SerializeField]
    private GameObject ItemButton;
    [SerializeField]
    private GameObject InstantePosition;
    [SerializeField]
    private GameObject ItemInfo;
    [SerializeField]
    private GameObject BagDialogNode;
    private List<UserItemData> itemDatasSave;

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventConfig.BagReset.ToString(), SetItemContent);
        EventManager.StartListening(EventManager.EventConfig.UseItem.ToString(), SetItemContent);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventConfig.BagReset.ToString(), SetItemContent);
        EventManager.StopListening(EventManager.EventConfig.UseItem.ToString(), SetItemContent);
    }
    public void SetInfo()
    {
        EventParam eventParam = new EventParam();
        this.ItemInfo.SetActive(false);
        this.SetItemContent(eventParam);
    }

    private void SetItemContent(EventParam eventParam)
    {
        List<UserItemData> itemDatas = DataManager.UserData.GetItemData();
        itemDatasSave = new List<UserItemData>();
        for (int x = 0; x < InstantePosition.transform.childCount; x++)
        {
            GameObject ItemButton = InstantePosition.transform.GetChild(x).gameObject;
            Destroy(ItemButton);
        }
        for (int i = 0; i < itemDatas.Count; i++)
        {
            var itemButton = Instantiate(ItemButton, InstantePosition.transform) as GameObject;
            itemButton.GetComponent<ItemButton>().SetInfo(itemDatas[i]);
            itemButton.transform.SetParent(InstantePosition.transform);
            itemButton.SetActive(true);
        }
        itemDatasSave = itemDatas;
    }

    public void CloseDialog() 
    {
        this.gameObject.SetActive(false);
    }
}
