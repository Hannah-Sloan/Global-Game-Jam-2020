using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Attributes
    private float startTime = -99;
    private float endTime = -99;
    private float internalTime = -99;

    private float timerLength = 0;

    private bool paused = false;
    #endregion

    #region Errors
    private bool createdCorrectly = false;

    private void NotifyIncorrectCreation()
    {
        Debug.LogError("Please use Timer.CreateTimer() and no other way to create a Timer", gameObject);
    }

    private void NotifyInitializationError()
    {
        Debug.LogError("This timer has not been initialized properly to perform that operation");
    }
    #endregion

    public static Timer CreateTimer(float time, bool startOnCreation = true)
    {
        GameObject gameObject = new GameObject("Timer Object");
        DontDestroyOnLoad(gameObject);
        Timer timer = gameObject.AddComponent<Timer>();

        if (startOnCreation)
        {
            timer.startTime = Time.time;
            timer.timerLength = time;
            timer.endTime = timer.startTime + time;
            timer.internalTime = timer.startTime;
        }
        else
        {
            timer.startTime = -99f;
            timer.endTime = -99f;
            timer.internalTime = -99f;
            timer.timerLength = time;
        }
        timer.paused = false;
        timer.createdCorrectly = true;

        return timer;
    }

    private void Update()
    {
        if (!createdCorrectly) NotifyIncorrectCreation();

        if (!paused && createdCorrectly && internalTime != -99f)
            internalTime += Time.deltaTime;
    }

    #region Methods

        #region Setters
        public void SetTimerLength(float timerLength)
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            this.timerLength = timerLength;
            if (endTime != -99f && startTime != -99f)
                endTime = startTime + timerLength;
            else
                NotifyInitializationError();
        }

        public void ResetTimer()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            startTime = Time.time;
            internalTime = startTime;
            endTime = startTime + timerLength;
        }

        public void TimerStart()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            startTime = Time.time;
            internalTime = startTime;
            endTime = startTime + timerLength;
            paused = false;
        }

        public void TimerStop()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            startTime = -99;
            endTime = -99;
        }

        public void Play()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            paused = false;
        }

        public void Pause()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            paused = true;
        }
        #endregion

        #region Getters
        public bool CheckComplete()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            if (endTime == -99 || internalTime == -99) NotifyInitializationError();

            return internalTime >= endTime;   
        }

        public float StartTime()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            if (startTime == -99) NotifyInitializationError();

            return startTime;
        }

        public float EndTime()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            if (endTime == -99) NotifyInitializationError();

            return endTime;
        }

        public float TimeRemaining()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            if (endTime == -99) NotifyInitializationError();

            return Mathf.Clamp(endTime - internalTime, 0, Mathf.Infinity);
        }

        public float TimeElapsed()
        {
            if (!createdCorrectly) NotifyIncorrectCreation();

            if (internalTime == -99) NotifyInitializationError();

            return internalTime;
        }
        #endregion

    public bool CheckAndReset()
    {
        if (!createdCorrectly) NotifyIncorrectCreation();

        if (endTime == -99)
            throw new System.Exception("Timer has no end time. Please use Start or ResetTimer");

        bool check = Time.time >= endTime;
        if (check)
            ResetTimer();

        return check;
    }
    #endregion

}
