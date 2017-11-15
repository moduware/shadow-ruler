using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ShadowRuler
{
    public class ShadowThread : IDisposable
    {
        public string Name => _name;

        private readonly string _name;
        private IShadowThreadBase _thread;
        private List<Action> _tasksList = new List<Action>();
        //private EventWaitHandle _waitHandle = new ManualResetEvent(initialState: false);

        public ShadowThread(string name, IShadowThreadBase thread)
        {
            _name = name;
            _thread = thread;
            _thread.Init(TaskRunner);
            _thread.Start();
        }

        public void AddTask(Action task)
        {
            _tasksList.Add(task);
            // Restarting thread
            //_waitHandle.Set();
            _thread.Resume();
        }

        private void TaskRunner()
        {
            //Thread.CurrentThread.Name = _name;
            _thread.SetName(_name);
            while(true)
            {
                if(_tasksList.Count == 0)
                {
                    // No stuff to do, pausing thread
                    //_waitHandle.Reset();
                    // And waiting for new task to be added
                    //_waitHandle.WaitOne();
                    _thread.Pause();
                    continue;
                }

                // taking task from list
                var task = _tasksList[0];
                // and removing it from list
                _tasksList.Remove(task);
                // executing task
                task();
            }
        }

        public void Dispose()
        {
            //_thread.Abort();
            //_thread = null;
            _thread.Destroy();
        }
    }
}
