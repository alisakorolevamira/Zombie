using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        public int LoadProgress(string key, int value)
        {
            return PlayerPrefs.GetInt(key, value);
        }

        public string LoadProgress(string key, string value)
        {
            return PlayerPrefs.GetString(key, value);
        }

        public void SaveProgress(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public void SaveProgress(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public void ResetProgress(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}