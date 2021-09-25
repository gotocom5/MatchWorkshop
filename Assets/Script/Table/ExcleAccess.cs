using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using FlexFramework.Excel;
using UnityEngine.Networking;
using System.Linq;

public class ExcleAccess : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadExcel();
            Debug.Log("load");
        }
    }

    public delegate void DownloadHandler(byte[] bytes);
    //���Jexcel
    public void LoadExcel()
    {
        // StreamingAssets
        StartCoroutine(LoadFileAsync("checkdata.xlsx", bytes =>
        {
            var book = new WorkBook(bytes);
            //Populate(book[0]);
            SetDataRow(book[0]);
        }));
    }
    //Ū���C��M�C��
    void SetDataRow(IEnumerable<Row> rows)
    {
        int count = rows.Count(r => !r.IsEmpty());
        if (count == 0)
            return;
        //�N�G���Ʀr�s��C�� �A�q�L��CŪ�� 
        List<Row> rowData = new List<Row>(rows);
        for (int j = 1; j < rowData.Count; j++)//��
        {
            for (int i = 0; i < rowData[j].Count; i++)//�C
            {
                Debug.Log(rowData[j][i].Text);
            }
        }
    }
    //�D�P�B���J
    private IEnumerator LoadFileAsync(string path, DownloadHandler handler)
    {
        // streaming assets should be loaded via web request
        // on WebGL/Android platforms, this folder is in a compressed directory
        var url = Path.Combine(Application.streamingAssetsPath, path);
        using (var req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();
            var bytes = req.downloadHandler.data;
            handler(bytes);
        }
    }
}
