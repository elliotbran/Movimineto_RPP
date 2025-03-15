using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Collision : MonoBehaviour
{
    public AudioSource myAudioSource;
    public AudioClip myAudioClip;

    private void OnCollisionEnter2D(Collision2D collision) // Simple script para reproducir audio al golpear un bloque.
    {
        myAudioSource.PlayOneShot(myAudioClip);
    }
}
