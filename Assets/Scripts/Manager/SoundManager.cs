using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum SoundType
    {
        drop,
        click,
        merge,
    }
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioSource soundSource;
        [SerializeField] AudioSource soundMusic;
        public List<AudioClip> listSound;

        public void PlaySound(SoundType type)
        {
            soundSource.clip = listSound[(int)type];
            soundSource.Play();
        }

        public void PlayMusic()
        {
            soundMusic.Play();
        }    
    }
}
