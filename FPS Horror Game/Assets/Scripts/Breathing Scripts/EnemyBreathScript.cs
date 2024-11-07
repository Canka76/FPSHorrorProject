using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBreathScript : BreatheAction
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] breathClips;
    [SerializeField] private float breathCooldown = 2.0f; // Cooldown duration in seconds
    
    private int randomClip;
    private float lastBreathTime;

    public override void OnFocusEnemy()
    {
        // Check if cooldown has elapsed
        if (Time.time - lastBreathTime >= breathCooldown)
        {
            // Play a random breathing sound if there are clips
            if (breathClips.Length > 0)
            {
                randomClip = Random.Range(0, breathClips.Length);
                _audioSource.PlayOneShot(breathClips[randomClip]);
                lastBreathTime = Time.time; // Reset cooldown timer
            }
            else
            {
                Debug.LogWarning("No breath clips assigned in EnemyBreathScript.");
            }
        }
    }

    public override void OnLoseFocusEnemy()
    {
        Debug.Log("Stopped looking at enemy: " + gameObject.name);
    }
}