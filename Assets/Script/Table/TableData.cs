using System.Collections.Generic;
using UnityEngine;
using FlexFramework.Excel;

public class TableData
{
    private Document document;
    private List<Cell> attributes;

    public TableData(string path)
    {
        this.document = new Document();
        this.attributes = new List<Cell>();
        LoadCSV(path);
    }

    private void LoadCSV(string path)
    {
        var doc = Document.LoadAt(path);
        this.document = doc;
        Row row = new Row();
        row = doc[0];
        foreach (Cell cell in row)
        {
            attributes.Add(cell);
        }
    }

    public Row GetAttributes()
    {
        return this.document[0];
    }

    public List<Row> GetAllData()
    {
        List<Row> allData = new List<Row>();

        for (int i = 0; i < document.Count; i++)
        {
            if (i > 0)
            {
                allData.Add(document[i]);
            }
        }
        return allData;
    }

    public Row GetDataById(int Id)
    {
        foreach (Row data in GetAllData())
        {
            if (Id == int.Parse(data[0]))
            {
                return data;
            }
        }
        Debug.LogError("Id¿é¤J¿ù»~");
        return null;
    }
}
