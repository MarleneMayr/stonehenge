using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    AudioSource audioSource;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 0.05)
        {
            audioManager.PlayImpactSound(audioSource, collision.relativeVelocity.magnitude);
        }
        else if (collision.rigidbody.isKinematic)
        {
            audioManager.PlayImpactSound(audioSource, Camera.main.velocity.magnitude);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            audioManager.PlayImpactSound(audioSource, collision.relativeVelocity.magnitude);
        }
    }
}
