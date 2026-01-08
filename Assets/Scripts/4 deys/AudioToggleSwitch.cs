using UnityEngine;

public class AudioToggleSwitch : MonoBehaviour
{
    [SerializeField] public float LowVolume = 0.3f;
    [SerializeField] private AudioMode Amod;
    
    private float volumeToApply;
    private enum AudioMode
    {
        AudioMin,
        AudioMax,
        AudioMute
    }      
    private void AudioSettngAmod()
    {
      
        switch (Amod)
        {
            case AudioMode.AudioMin:
                volumeToApply = LowVolume;
                Amod = AudioMode.AudioMax;
                break;

            case AudioMode.AudioMax:
                volumeToApply = 1.0f;
                Amod = AudioMode.AudioMute;
                break;

            case AudioMode.AudioMute:
                volumeToApply = 0.0f;
                Amod = AudioMode.AudioMin;
                break;

            default:
                Amod = AudioMode.AudioMin;
                break;
        }
    }    
    public void ApplyVolume()
    {
        AudioSettngAmod();
        AudioListener.volume = volumeToApply;               
        Debug.Log($"Volume: {volumeToApply}");
    }
}
  


