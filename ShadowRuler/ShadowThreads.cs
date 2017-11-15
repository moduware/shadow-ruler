using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowRuler
{
    public static class ShadowThreads
    {
        private static List<ShadowThread> _threadsList = new List<ShadowThread>();
        private static IShadowThreadBase _threadFactory = null;

        public static void SetFactory(IShadowThreadBase threadFactory)
        {
            _threadFactory = threadFactory;
        }

        public static void RunOnThread(string threadName, Action code)
        {
            var thread = _threadsList.Find((th) => th.Name == threadName);
            if (thread == null)
            {
                throw new KeyNotFoundException("Unknow thread: " + threadName);
            }

            thread.AddTask(code);
        }

        public static void CreateThread(string threadName)
        {
            if(_threadFactory == null)
            {
                throw new MissingMemberException("You need provide thread factory, before creating threads");
            }
            var thread = new ShadowThread(threadName, _threadFactory.Create());
            _threadsList.Add(thread);
        }

        public static void StopAllThreads()
        {
            foreach (var thread in _threadsList)
            {
                thread.Dispose();
            }
        }
    }
}
