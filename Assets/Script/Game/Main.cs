using DG.Tweening;
using FlexFramework.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private GameObject BagDialog;
    [SerializeField]
    private GameObject hint;

    private void Start()
    {
        this.Hint();
    }
    public void SaveButton()
    {
        DataManager.UserData.SaveData();
    }
    public void OpenBagButton()
    {
        if (this.BagDialog.activeSelf == false)
        {
            this.BagDialog.GetComponent<BagDialog>().SetInfo();
            this.BagDialog.SetActive(true);
        }
        else
        {
            this.BagDialog.SetActive(false);
        }
    }

    public void Hint()
    {
        hint.transform.DOMove(new Vector2(hint.transform.position.x + 1500, hint.transform.position.y), 0.5f).SetEase(Ease.OutBack);
        if (hint.transform.position.x > -100)
        {
            Destroy(hint.gameObject, 0.6f);
        }
    }
}
