using UnityEngine;

public class Settings {

    #region Singleton

    private static Settings _instance;
    public static Settings Instance => _instance ??= new Settings();

    #endregion

    private Settings() {
        Load();
    }

    public static float RaycastDistance => 10f;

    public float Sensitivity { get; set; } = 0.7f;

    public float Volume { get; set; } = 0.75f;

    private void Load() {
        if (PlayerPrefs.HasKey("Volume")) {
            Volume = PlayerPrefs.GetFloat("Volume");
        }

        if (PlayerPrefs.HasKey("Sensitivity")) {
            Sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        }
    }

    public void Save() {
        PlayerPrefs.SetFloat("Volume", Volume);
        PlayerPrefs.SetFloat("Sensitivity", Sensitivity);

        PlayerPrefs.Save();
    }
}
