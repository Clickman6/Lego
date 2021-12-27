using System;
using UnityEngine;

[Serializable]
public class CustomAudio {
    public AudioClip Clip;
    public float Volume = 1f;

    public float GetVolume() {
        return Volume * Settings.Instance.Volume;
    }
}
