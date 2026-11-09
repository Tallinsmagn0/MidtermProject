using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Menu Sounds")]
    public AudioClip menuMusic;
    public AudioClip menuClickSound;

    [Header("Play Sounds")]
    public AudioClip playMusic;

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void AddClickSoundToButtonsInScene()
    {
        Button[] buttons = FindObjectsByType<Button>(
            FindObjectsInactive.Include, 
            FindObjectsSortMode.None
        );
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(ClickButton);
        }
    }

    public void ClickButton()
    {
        PlaySFX(menuClickSound);
    }
}
