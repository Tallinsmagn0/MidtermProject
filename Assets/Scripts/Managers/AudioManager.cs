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
    public AudioClip gameMusic;

    public bool IsPlayingMusic()
    {
        return musicSource.isPlaying;
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

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }

    public void ClickButton()
    {
        PlaySFX(menuClickSound);
    }

    private void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
