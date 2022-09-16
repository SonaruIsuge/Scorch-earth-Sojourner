using System.Timers;
using UnityEngine;

namespace SonaruUtilities
{
    public class SimpleTimer
    {
        private float targetTime;
        private float finishTime;

        private bool IsPause;
        
        private float remain;

        public float Remain => IsPause ? remain : Mathf.Max(finishTime - Time.time, 0);

        public float Remain01 => Remain / targetTime;
        
        public bool IsFinish => Time.time >= finishTime;

        
        public SimpleTimer(float timer)
        {
            targetTime = timer;
            finishTime = timer + Time.time;
            IsPause = false;
        }

        public void Pause()
        {
            if(IsPause) return;
            IsPause = true;
            remain = finishTime - Time.time;
        }

        public void Resume()
        {
            if(!IsPause) return;
            IsPause = false;
            finishTime = remain + Time.time;
        }

        public void Reset()
        {
            finishTime = targetTime + Time.time;
            IsPause = false;
        }

        public void Reset(float newTimer)
        {
            targetTime = newTimer;
            finishTime = newTimer + Time.time;
            IsPause = false;
        }

        public void Reduce(float reduceTime)
        {
            finishTime -= reduceTime;
        }
    }
}