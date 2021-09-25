using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject MessageDialog = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (MessageDialog.activeSelf == false && DataManager.UserData.GetItemByID(1010009) != null)
            {
                MessageDialog.SetActive(true);
            }
        }
    }
}
