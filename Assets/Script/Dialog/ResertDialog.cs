using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResertDialog : MonoBehaviour
{
    public void ConfirmButton() 
    {
        EventParam eventParam = new EventParam();
        EventManager.TriggerEvent(EventManager.EventConfig.GameResert.ToString(), eventParam);
        this.CancelButton();
    }
    public void CancelButton() 
    {
        this.gameObject.SetActive(false);
    }
}
