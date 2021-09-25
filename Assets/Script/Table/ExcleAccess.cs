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
    //載入excel
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
    //讀取列表和列表
    void SetDataRow(IEnumerable<Row> rows)
    {
        int count = rows.Count(r => !r.IsEmpty());
        if (count == 0)
            return;
        //將二維數字存到列表 ，通過行列讀取 
        List<Row> rowData = new List<Row>(rows);
        for (int j = 1; j < rowData.Count; j++)//行
        {
            for (int i = 0; i < rowData[j].Count; i++)//列
            {
                Debug.Log(rowData[j][i].Text);
            }
        }
    }
    //非同步載入
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
