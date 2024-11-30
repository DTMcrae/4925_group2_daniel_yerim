using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source --------")]
    [SerializeField] AudioSource musicSouce;
    [SerializeField] AudioSource SFXSource;

    [Header("-------- Audio Clip --------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip goodCatch;
    public AudioClip badCatch;
    public AudioClip levelComplete;

    private void Start()
    {
        musicSouce.clip = background;
        musicSouce.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        if (musicSouce.isPlaying)
        {
            musicSouce.Stop();
        }
    }
}
