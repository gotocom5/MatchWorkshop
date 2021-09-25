using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������s�C���i�סA�ӬO�̷�e���A�M�w������
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
