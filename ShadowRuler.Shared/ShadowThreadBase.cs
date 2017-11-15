using ShadowRuler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShadowTester
{
    public class ShadowThreadBase : IShadowThreadBase
    {
        public bool Active => _active;

        private Thread _thread;
        private bool _active = false;
        private EventWaitHandle _waitHandle = new ManualResetEvent(initialState: false);

        public ShadowThreadBase()
        {

        }

        public IShadowThreadBase Create()
        {
            return new ShadowThreadBase();
        }

        public void Destroy()
        {
            _thread.Abort();
            _thread = null;
        }

        public void Init(Action action)
        {
            _thread = new Thread(o => action());
        }

        public void Pause()
        {
            _waitHandle.Reset();
            _active = false;
            _waitHandle.WaitOne();
        }

        public void Resume()
        {
            _waitHandle.Set();
            _active = true;
        }

        public void SetName(string threadName)
        {
            Thread.CurrentThread.Name = threadName;
        }

        public void Start()
        {
            _thread.Start();
        }
    }
}
