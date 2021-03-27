using UnityEngine;

public class PrestartAudioSetting : MonoBehaviour
{

    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private float backgroundFloat, soundEffectsFloat;
    public AudioSource[] backgroundAudio;
    public AudioSource[] soundEffectsAudio;

    // Start is called before the first frame update
    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        for (int h = 0; h < backgroundAudio.Length; h++)
        {
            backgroundAudio[h].volume = PlayerPrefs.GetFloat(BackgroundPref);
        }

        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = PlayerPrefs.GetFloat(SoundEffectsPref);
        }
    }
}
