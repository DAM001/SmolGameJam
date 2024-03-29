using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBase : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public float Volume
    {
        get
        {
            return _audioSource.volume;
        }
        set
        {
            _audioSource.volume = value;
        }
    }

    public void PlaySoundWithoutPitch(AudioClip sound)
    {
        if (sound == null) return;

        _audioSource.clip = sound;
        _audioSource.Play();
    }

    public void PlaySound(AudioClip sound)
    {
        if (sound == null) return;

        _audioSource.pitch = Random.Range(.8f, 1.1f);
        PlaySoundWithoutPitch(sound);
    }
}
