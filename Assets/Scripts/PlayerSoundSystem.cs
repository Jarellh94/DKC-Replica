using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundSystem : MonoBehaviour
{
    public AudioClip jumpClip;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound() {
        float randomNum = Random.Range(0.75f, 1.25f);
        audioSource.pitch = randomNum;
        audioSource.Play();
    }
}

