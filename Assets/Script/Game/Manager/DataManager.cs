using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static UserDataController UserData;

    private static DataManager getInstance;
    public static DataManager GetInstance
    {
        get
        {
            if (getInstance == null)
            {
                getInstance = FindObjectOfType(typeof(DataManager)) as DataManager;
                if (getInstance == null)
                {
                    GameObject go = new GameObject("DataManager");
                    getInstance = go.AddComponent<DataManager>();
                }
            }
            return getInstance;
        }
    }
    private void Awake()
    {
        UserData = new UserDataController();
    }
}
