using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cooldown
{
    public enum Progress
    {
        Ready,   // == 0
        Started,   // == 1
        Inprogress,   // == 2
        Finished   // == 3
    }

    public Progress CurrentProgress = Progress.Ready;

    public float Duration = 1.0f; //Duration between each shot
    public float TimeLeft
    {
        get
        {
            return _currentDuration;
        }
    }
    public bool IsOnCooldown
    {
        get
        {
            return _isOnCooldown;
        }
    }
    
    private float _currentDuration = 0f; //after a shot, the timer resets
    private bool _isOnCooldown; //after shoot, it pauses/resumes

    private Coroutine _coroutine;


    public void StartCooldown()
    {
        if (CurrentProgress is Progress.Started or Progress.Inprogress)
            return;

        _coroutine = CoroutineHost.Instance.StartCoroutine(OnCoolDown());
    }
    public void StopCooldown()
    {
        if(_coroutine != null)
            CoroutineHost.Instance.StopCoroutine(_coroutine);

        _currentDuration = 0f;
        _isOnCooldown = false;
        CurrentProgress = Progress.Ready;
    }
    IEnumerator OnCoolDown()
    {
        CurrentProgress = Progress.Started;
        _currentDuration = Duration;
        _isOnCooldown = true;

        while(_currentDuration > 0)
        {
            _currentDuration -= Time.deltaTime;
            CurrentProgress = Progress.Inprogress;

            yield return null;
        }

        _currentDuration = 0f;
        _isOnCooldown = false;

        CurrentProgress = Progress.Finished;
    }
}
