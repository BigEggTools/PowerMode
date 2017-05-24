namespace BigEgg.Tools.PowerMode
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ThrottledAction<T> where T : class
    {
        private readonly object timerLock = new object();
        private readonly TaskScheduler taskScheduler;
        private Timer timer;

        private readonly Action<T> action;
        private readonly int delayTime;


        public ThrottledAction(Action<T> action, int delayTime)
        {
            if (action == null) { throw new ArgumentNullException("action"); }

            this.action = action;
            this.delayTime = delayTime;

            this.taskScheduler = SynchronizationContext.Current != null ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Default;

        }


        public bool IsRunning { get; private set; }


        public void InvokeAccumulated(T parameter)
        {
            if (!IsRunning)
                lock (timerLock)
                    if (!IsRunning)
                    {
                        if (timer == null)
                        {
                            timer = new Timer(TimerCallbackHandler, parameter, 0, delayTime);
                        }
                        else
                        {
                            timer.Change(0, delayTime);
                        }
                    }
        }

        public void Cancel()
        {
            if (IsRunning)
                lock (timerLock)
                    if (IsRunning)
                    {
                        IsRunning = false;
                        timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
                    }
        }

        private void TimerCallbackHandler(object state)
        {
            if (IsRunning)
                lock (timerLock)
                    if (IsRunning)
                    {
                        IsRunning = false;
                        timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
                        return;
                    }

            IsRunning = true;
            Task.Factory.StartNew(() => action(state as T), CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler);
        }
    }
}
