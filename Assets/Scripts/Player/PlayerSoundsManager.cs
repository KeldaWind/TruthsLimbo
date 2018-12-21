using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSoundsManager {
    [SerializeField] AudioSource stepSource;
    [SerializeField] AudioClip[] stepSounds;
    [SerializeField] float numberOfStepsPerSeconds;
    float currentStepTimer;

    public void SetUp()
    {
        currentStepTimer = 1 / numberOfStepsPerSeconds;
    }

    public void UpdateStepSound(bool running)
    {
        if (currentStepTimer > 0)
            currentStepTimer -= Time.deltaTime * (running ? 1.5f : 1);
        else if(currentStepTimer < 0)
        {
            EmitStepSound();
            currentStepTimer = 1/numberOfStepsPerSeconds;
        }
    }

    public void EmitStepSound()
    {
        stepSource.clip = stepSounds[Random.Range(0, stepSounds.Length)];
        stepSource.Play();
    }
}
