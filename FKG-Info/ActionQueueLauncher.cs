﻿using System;
using System.Collections.Generic;
using System.Threading;



namespace FKG_Info
{
    class ActionQueueLauncher
    {
        private List<Action> Actions;

        private int Count;
        private int MaxCount;
        private int TotalCounter;

        private LifeContcol Life;

        private EventWaitHandle WakeUp;

        private object Locker = new object();


        public int InQueue { get { return Actions.Count; } }



        public ActionQueueLauncher(LifeContcol life, int maxcount = 4)
        {
            Actions = new List<Action>();

            WakeUp = new EventWaitHandle(false, EventResetMode.ManualReset);

            MaxCount = maxcount;
            TotalCounter = 0;
            Life = life;

            Thread th = new Thread(AutoLauncher);
            th.Name = "Action Auto Launcher";
            th.Start();
        }



        private void AutoLauncher()
        {
            while (Life.IsLife)
            {
                if ((Actions.Count == 0) || (Count >= MaxCount))
                {
                    WakeUp.Reset();
                    WakeUp.WaitOne(1000);
                    continue;
                }

                Action action;
                Thread thread;

                lock (Locker)
                {
                    action = Actions[0];
                    Actions.Remove(action);
                }

                TotalCounter++;

                thread = new Thread(() => DoAction(action));
                thread.Name = "Auto Launched " + TotalCounter.ToString();
                thread.Start();
            }

            while (Count > 0) Thread.Sleep(200);
        }



        /// <summary>
        /// Next action
        /// </summary>
        /// <param name="action"></param>
        private void DoAction(Action action)
        {
            lock (Locker) Count++;
            action();
            lock (Locker) Count--;
            WakeUp.Set();
        }



        /// <summary>
        /// Add action (function) to queue
        /// </summary>
        /// <param name="action"></param>
        public void Add(Action action)
        {
            lock (Locker) Actions.Add(action);

            WakeUp.Set();
        }



        public bool IsStopped()
        {
            if (Life.IsLife) return false;
            if (Count != 0) return false;
            return true;
        }
    }
}
