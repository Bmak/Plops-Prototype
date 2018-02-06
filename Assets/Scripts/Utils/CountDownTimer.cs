using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Timer utility class that tracks the remaining time between the current
/// time and when the timer finishes
/// </summary>
public class CountDownTimer : MonoBehaviour
{
    //private CoroutineCreator _coroutineCreator = CoroutineCreator.Instance;

    private IEnumerator _countDownCoroutine = null;

    private bool _fixedTimer = false;

    /// <summary>
    /// Starts the count down timer
    /// </summary>
    /// <param name="numSeconds">Timer finishes at current time plus numSeconds</param>
    /// <param name="updateCallback">Called once a second, providing the remaining time to the callback</param>
    /// <param name="finishedCallback">Called when the timer finishes</param>
    public void StartTimer(int numSeconds, Action<long> updateCallback, Action finishedCallback)
    {
        _fixedTimer = false;
        long endTime = CurrentTime() + numSeconds;
        StartTimer(endTime, updateCallback, finishedCallback);
    }

    public void StartFixedTimer(int numSeconds, Action<long> updateCallback, Action finishedCallback)
    {
        _fixedTimer = true;
        long endTime = CurrentTime() + numSeconds;
        StartTimer(endTime, updateCallback, finishedCallback);
    }

    /// <summary>
    /// Starts the count down timer
    /// </summary>
    /// <param name="endTime">When the timer should finish</param>
    /// <param name="updateCallback">Called once a second, providing the remaining time to the callback</param>
    /// <param name="finishedCallback">Called when the timer finishes</param>
    private void StartTimer(long endTime, Action<long> updateCallback, Action finishedCallback)
    {
        StopTimer();
        _countDownCoroutine = CountDown(endTime, updateCallback, finishedCallback);
        StartCoroutine(_countDownCoroutine);
    }

    public void StopTimer()
    {
        if (_countDownCoroutine != null)
        {
            StopCoroutine(_countDownCoroutine);
            _countDownCoroutine = null;
        }
    }

    private long CurrentTime()
    {
        return (long)Time.time;
    }

    private IEnumerator CountDown(long endTime, Action<long> updateCallback, Action finishedCallback)
    {
        //required to use _PlayerDC.GetServerTimeWithDebugOffset so the Timeshift cheat works correctly
        long remainingTime = endTime - CurrentTime();

        while (remainingTime > 0)
        {
            if (updateCallback != null)
            {
                updateCallback(remainingTime);
            }
            if (_fixedTimer)
            {
                yield return new WaitForFixedUpdate();
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
            remainingTime = endTime - CurrentTime();
        }

        _countDownCoroutine = null;
        if (finishedCallback != null)
        {
            finishedCallback();
        }
    }
}
