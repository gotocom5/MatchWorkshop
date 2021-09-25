using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // ªì©l¤ÆºX¼Ð
    private bool isInitialization = true;
    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventConfig.GameProgress.ToString(), GameProgress);
        EventManager.StartListening(EventManager.EventConfig.GameResert.ToString(), ResertProgress);
    }
    private void Start()
    {
        DataManager.UserData.LoadData();
    }

    private void ResertProgress(EventParam eventParam)
    {
        if (DataManager.UserData.GetItemData().Count == 9)
        {
            DataManager.UserData.SetDefaultItemData();
            isInitialization = true;
        }
    }
    private void GameProgress(EventParam eventParam)
    {
        List<UserItemData> Items = DataManager.UserData.GetItemData();
        if (Items.Count < 6)
        {
            var defaultItem = DataManager.UserData.getDefaultItem();
            for (int i = 0; i < defaultItem.Count; i++)
            {
                DataManager.UserData.AddItemData(defaultItem[i].id, 1);
            }
            return;
        }

        if (Items.Count == 6 && isInitialization)
        {
            StartCoroutine(InstantiateItem(1010001, 1));
            isInitialization = false;
        }
        if (Items.Count == 7)
        {
            StartCoroutine(InstantiateItem(1010007, 1));
        }
        if (Items.Count == 8)
        {
            StartCoroutine(InstantiateItem(1010009, 1));
        }

    }
    private IEnumerator InstantiateItem(int Id, int Count)
    {
        var plyer = GameObject.FindWithTag("Player");
        Vector2 InstantiatePosition = new Vector2(plyer.transform.position.x - 8, plyer.transform.position.y + 8);
        GameObject Item = Resources.Load("Prefeb/Item/Item") as GameObject;
        Item.GetComponent<Cure>().SetInfo(Id, Count);
        Instantiate(Item, InstantiatePosition, Quaternion.identity);
        yield return new WaitForSeconds(1f);
    }

}