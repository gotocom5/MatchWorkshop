using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 不直接重製遊戲進度，而是依當前狀態決定掉落物
        if (collision.gameObject.CompareTag("Player"))
        {
            var Item = GameObject.Find("Item(Clone)");
            if (Item == null)
            {
                EventParam eventParam = new EventParam();
                EventManager.TriggerEvent(EventManager.EventConfig.GameProgress.ToString(), eventParam);
            }
        }
    }
}
