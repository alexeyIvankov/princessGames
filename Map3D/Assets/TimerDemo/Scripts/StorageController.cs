using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TimerComponents
{
    public static class StorageController
    {
        public static void UpdateDataInStorage(Dictionary<string, TimerModel> timers)
        {
            var timersJson = JsonUtility.ToJson(new TimerModelCollection(timers.Values.ToArray()));
            using (var sw = new StreamWriter("storage.txt", false))
            {
                sw.WriteLine(timersJson);
                sw.Close();
            }
            
        }
        
        public static Dictionary<string, TimerModel> RestoreDataFromStorage()
        {
            if (!File.Exists("storage.txt"))
            {
                File.Create("storage.txt");
                return new Dictionary<string, TimerModel>();
            }
            using (var sr = new StreamReader("storage.txt"))
            {
                var timersJson = sr.ReadLine();
                sr.Close();
                var timerModelCollection = JsonUtility.FromJson<TimerModelCollection>(timersJson);
                var models = timerModelCollection.TimerModels.ToDictionary(timerModel => timerModel.Name);
                return models;
            }
        }
    }
}