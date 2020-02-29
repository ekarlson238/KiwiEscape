using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BackgroundTrack
{
    Menu,
    Gameplay
}
public enum SoundEffect
{
    KiwiHurt,
    KiwiJump,
    KiwiEat,
    KiwiTweet
}

public sealed class AudioManager : MonoBehaviour
{
    private static AudioManager self;

    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SFXSource;

    [SerializeField] private AudioClip menuBGM;
    [SerializeField] private AudioClip gameplayBGM;

    [SerializeField] private AudioClip[] kiwiHurt;
    [SerializeField] private AudioClip[] kiwiJump;
    [SerializeField] private AudioClip[] kiwiEat;
    [SerializeField] private AudioClip[] kiwiTweet;

    private Dictionary<BackgroundTrack, AudioClip> BGM;
    private Dictionary<SoundEffect, AudioClip[]> SFX;

    private void Start()
    {
        BGM = new Dictionary<BackgroundTrack, AudioClip>();
        SFX = new Dictionary<SoundEffect, AudioClip[]>();

        self = this;
        BGMSource.loop = true;
        SFXSource.loop = false;
        //BGM[BackgroundTrack.Menu] = menuBGM;
        BGM[BackgroundTrack.Gameplay] = gameplayBGM;
        SFX[SoundEffect.KiwiHurt] = kiwiHurt;
        SFX[SoundEffect.KiwiJump] = kiwiJump;
        SFX[SoundEffect.KiwiEat] = kiwiEat;
        SFX[SoundEffect.KiwiTweet] = kiwiTweet;
    }

    public static void SetBackgroundMusic(BackgroundTrack track)
    {
        self.BGMSource.clip = self.BGM[track];
        self.BGMSource.Play();
    }

    public static void PlaySoundEffect(SoundEffect effect)
    {
        self.SFXSource.clip = self.RandomArrayElement(self.SFX[effect]);
        self.SFXSource.Play();
    }

    private T RandomArrayElement<T>(T[] array)
    {
        if(array.Length == 0) { return default; }
        int randomIndex = Random.Range(0, array.Length);
        return array[randomIndex];
    }
}
