using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : AudioBase
{
    [SerializeField] private AudioClip[] _musics;
    [Range(0f, 1f)] [SerializeField] private float _volume;

    private void Start()
    {
        Volume = _volume;

        PlayMusicById(0);
    }

    public void PlayMusicById(int id)
    {
        if (id >= _musics.Length) return;

        PlaySound(_musics[id]);
    }
}
