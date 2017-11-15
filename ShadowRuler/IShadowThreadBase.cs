using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRuler
{
    public interface IShadowThreadBase
    {
        bool Active { get; }

        IShadowThreadBase Create();
        void Init(Action action);
        void SetName(string threadName);
        void Start();
        void Pause();
        void Resume();
        void Destroy();
    }
}
