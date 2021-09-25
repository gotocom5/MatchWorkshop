using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        DataManager.UserData.SetDefaultItemData();
    }
    private void GameProgress(EventParam eventParam)
    {
        var Items = DataManager.UserData.GetItemData();
        if (Items.Count < 6)
        {
            var defaultItem = new List<UserItemData>
       {
       new UserItemData() { id = 1010002, count = 7, },
       new UserItemData() { id = 1010003, count = 6, },
       new UserItemData() { id = 1010004, count = 3, },
       new UserItemData() { id = 1010005, count = 8, },
       new UserItemData() { id = 1010006, count = 5, },
       new UserItemData() { id = 1010008, count = 2, },
       };
            for (int i = 0; i < defaultItem.Count; i++)
            {
                DataManager.UserData.AddItemData(defaultItem[i].id, 1);
            }
            return;
        }
        var whiteChocolate = DataManager.UserData.GetItemByID(1010001);
        var whiteCure = DataManager.UserData.GetItemByID(1010007);
        var key = DataManager.UserData.GetItemByID(1010009);
        if (key != null)
        {
            return;
        }

        if (Items.Count == 6 && whiteChocolate == null)
        {
            StartCoroutine(InstantiateItem(1010001, 1));
            return;
        }
        if (whiteCure != null)
        {
            StartCoroutine(InstantiateItem(1010009, 1));
            return;
        }
        if (whiteChocolate != null)
        {
            StartCoroutine(InstantiateItem(1010007, 1));
            return;
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