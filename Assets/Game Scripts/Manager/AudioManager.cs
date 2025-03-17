using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip, float VolumeScale) {
        audioSource.PlayOneShot(clip, VolumeScale);
    }
}