using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Helpers
{
    public class TimerHelper
    {
        Action act;
        int frameTime;
        int currentFrameTime;

        public static List<TimerHelper> Timers = new List<TimerHelper>();

        public TimerHelper(int framesBetween, Action action)
        {
            frameTime = framesBetween;
            act = action;
            currentFrameTime = 0;
            Timers.Add(this);
        }

        public void Delete()
        {
            Timers.Remove(this);
        }

        public void Update()
        {
            if (currentFrameTime == frameTime)
            {
                act?.Invoke();
                currentFrameTime = 0;
            }
            else currentFrameTime++;
        }
    }
}
