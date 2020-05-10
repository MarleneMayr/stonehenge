using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    [SerializeField] AudioClip sound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            float volumeScale = Mathf.Max(collision.relativeVelocity.magnitude / 10.0f, 1f);
            audioSource.PlayOneShot(sound, volumeScale);
            // TODO vary impact sounds and pitch
            // use different sound depending on material (floor/brick)
            // maybe: play sound only on one brick if two bricks collide
            // play in audiomanager or here?0
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            float volumeScale = Mathf.Max(collision.relativeVelocity.magnitude / 10.0f, 1f);
            audioSource.PlayOneShot(sound, volumeScale);
            // TODO vary impact sounds and pitch
        }
    }
}
