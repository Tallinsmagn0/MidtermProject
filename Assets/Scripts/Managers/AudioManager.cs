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

    [Header("Global Sounds")]
    public AudioClip gameMusic;

    [Header("Player Sounds")]
    public AudioClip jumpSound;
    public AudioClip footstepSound;
    public AudioClip castSpellSound;

    [Header("Door Sounds")]
    public AudioClip doorKnockSound;
    public AudioClip doorOpenSound;

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
            button.onClick.AddListener(PlayClickButtonSound);
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

    public void PlayClickButtonSound()
    {
        PlaySFX(menuClickSound);
    }

    public void PlayJumpSound()
    {
        PlaySFX(jumpSound);
    }

    public void PlayFootstepSound()
    {
        PlaySFX(footstepSound);
    }

    public void PlayCastSpellSound()
    {
        PlaySFX(castSpellSound);
    }

    public void PlayDoorKnockSound()
    {
        PlaySFX(doorKnockSound);
    }

    public void PlayDoorOpenSound()
    {
        PlaySFX(doorOpenSound);
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
