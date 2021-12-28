using System.IO;
using UnityEngine;

namespace SaveLoad {
    public static class SaveLoad {
        private static string _path = Application.persistentDataPath;
        private static string _name = "saved.json";

        public static void SaveFile() {
            string json = JsonUtility.ToJson(Playground.Current);
            
            File.WriteAllText(GetFullPath(), json);
            Debug.LogWarning("Save");
        }

        public static void LoadFile() {
            if (!HasFile()) return;
            
            string json = File.ReadAllText(GetFullPath());

            JsonUtility.FromJsonOverwrite(json, Playground.Current);
        }

        public static bool HasFile() {
            return File.Exists(GetFullPath());
        }

        public static string GetFullPath() {
            return _path + "/" + _name;
        }
    }
}
