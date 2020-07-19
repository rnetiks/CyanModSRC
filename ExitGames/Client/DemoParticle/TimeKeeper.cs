namespace ExitGames.Client.DemoParticle
{
    using System;
    using System.Runtime.CompilerServices;

    public class TimeKeeper
    {
        private int lastExecutionTime = Environment.TickCount;
        private bool shouldExecute;

        public TimeKeeper(int interval)
        {
            this.IsEnabled = true;
            this.Interval = interval;
        }

        public void Reset()
        {
            this.shouldExecute = false;
            this.lastExecutionTime = Environment.TickCount;
        }

        public int Interval { get; set; }

        public bool IsEnabled { get; set; }

        public bool ShouldExecute
        {
            get
            {
                if (!this.IsEnabled)
                {
                    return false;
                }
                if (!this.shouldExecute)
                {
                    return ((Environment.TickCount - this.lastExecutionTime) > this.Interval);
                }
                return true;
            }
            set
            {
                this.shouldExecute = value;
            }
        }
    }
}

