using DG.Tweening;
using FlexFramework.Excel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : MonoBehaviour
{
    [SerializeField]
    private int ItemId;
    [SerializeField]
    private int ItemCount;
    // Á×§K­«½Æ¤W¤É
    private bool isDead = false;
    public void SetInfo(int Id, int Count)
    {
        this.ItemId = Id;
        this.ItemCount = Count;
        Row ItemData = TableDataManager.ItemTool.GetDataById(Id);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>($"Art/ItemTool/{ItemData[1]}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            this.isDead = true;
            this.transform.DOMoveY(this.transform.position.y + 2, 0.5f);
            DataManager.UserData.AddItemData(this.ItemId, this.ItemCount);
            Destroy(this.gameObject, 0.5f);
            EventParam eventParam = new EventParam();
            EventManager.TriggerEvent(EventManager.EventConfig.GameProgress.ToString(), eventParam);
        }
    }
}
