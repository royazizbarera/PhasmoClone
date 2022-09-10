using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Infrastructure.Services
{
    public class DataSaveLoader : IService
    {

        public StoredInfo _storedInfo;

        [ContextMenu("Load")]
        public void LoadInfo()
        {
            if (!File.Exists(Application.streamingAssetsPath + "/data.json"))
            {
                File.Create(Application.streamingAssetsPath + "/data.json");
                Debug.Log("Create");
            }
            _storedInfo = JsonUtility.FromJson<StoredInfo>(File.ReadAllText(Application.streamingAssetsPath + "/data.json"));
        }
        [ContextMenu("Save")]
        public void SaveInfo()
        {
            File.WriteAllText(Application.streamingAssetsPath + "/data.json", JsonUtility.ToJson(_storedInfo));
        }

        public void SaveItemsAmount(int[] items)
        {
            _storedInfo.ItemsAmount = items;
            SaveInfo();
        }
        public void SaveMoney(float money)
        {
            _storedInfo.Money = money;
            SaveInfo();
        }


        [System.Serializable]
        public class StoredInfo
        {
            public float Money = 0f;
            public int[] ItemsAmount;
        }
    }
}
