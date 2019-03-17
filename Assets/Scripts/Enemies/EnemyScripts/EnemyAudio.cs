using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioSource[] playerSpotedSounds;
    public AudioSource[] attackSounds;
    public AudioSource[] idleSounds;

    [Header("Idle sound paraneters")]
    public bool canProduceIdleSound = true;
    [SerializeField] float soundFrequency = 5.0f;
    float countdown;
    [SerializeField, Range(0.0f,1.0f)] float soundChance = 0.5f;

    public void PlayEnemySound(AudioSource[] _soundArray)
    {
        int ran = Random.Range(0, _soundArray.Length);
        _soundArray[ran].Play();
    }

    private void Update()
    {
        IdleSound();
    }

    void IdleSound()
    {
        if (!canProduceIdleSound) return;

        countdown = Time.deltaTime;

        if(countdown >= soundFrequency)
        {
            float ran = Random.Range(0.0f, 1.0f);

            if(ran <= soundChance)
            {
                PlayEnemySound(idleSounds);
            }

            countdown = 0.0f;
        }
    }


}
