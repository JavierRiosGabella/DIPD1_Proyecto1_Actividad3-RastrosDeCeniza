using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_AudioPlayer : MonoBehaviour {

    [SerializeField] AudioClip[] audios = null;

    AudioListener audioListener;

    void Awake()
    {
        audioListener = FindObjectOfType<AudioListener>();
    }

    public void UP_PlayAudio()
    {
        AudioClip clip = audios[Random.Range(0, audios.Length)];
        AudioSource.PlayClipAtPoint(clip, audioListener.transform.position);
    }

}
