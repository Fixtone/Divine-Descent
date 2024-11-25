using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    #region SINGLETON

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private AudioSource audioSource;
    [SerializeField] private AudioClip clipMove;
    [SerializeField] private AudioClip clipAttack;
    [SerializeField] private AudioClip clipHit;
    [SerializeField] private AudioClip clipSpell;
    [SerializeField] private AudioClip clipRanged;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Play the sound effect for movement
    /// </summary>
    public void PlayMove()
    {
        audioSource.PlayOneShot(clipMove);
    }

    /// <summary>
    /// Play the sound effect for attacking
    /// </summary>
    public void PlayAttack()
    {
        audioSource.PlayOneShot(clipAttack);
    }

    /// <summary>
    /// Play the sound effect for being hit
    /// </summary>
    public void PlayHit()
    {
        audioSource.PlayOneShot(clipHit);
    }

    /// <summary>
    /// Play the sound effect for casting a spell
    /// </summary>
    public void PlaySpell()
    {
        audioSource.PlayOneShot(clipSpell);
    }

    /// <summary>
    /// Play the sound effect for a ranged attack
    /// </summary>
    public void PlayRanged()
    {
        audioSource.PlayOneShot(clipRanged);
    }
}
