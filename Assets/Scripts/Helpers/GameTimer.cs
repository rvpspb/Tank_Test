using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace tank.helpers
{
    public class GameTimer
    {        
        public float TargetTime { get; private set; }
        public bool IsRunning { get; private set; }

        public event Action OnFinish;

        CancellationTokenSource _cancelSource;

        public GameTimer(float targetTime)
        {            
            TargetTime = targetTime;                   
        }

        public void SetTargetTime(float time)
        {
            TargetTime = time;
        }

        public void Start()
        {     
            if (IsRunning)
            {               
                return;
            }

            _cancelSource = new CancellationTokenSource();
            IsRunning = true;
            WaitTime(_cancelSource.Token);            
        }

        public void Start(Action callback)
        {
            if (IsRunning)
            {
                return;
            }

            _cancelSource = new CancellationTokenSource();
            IsRunning = true;
            WaitTime(_cancelSource.Token).ContinueWith(callback);
        }

        public void Stop()
        {
            IsRunning = false;
            _cancelSource.Cancel();            
        }

        private async UniTask WaitTime(CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(TargetTime), cancellationToken: cancellationToken);

            if (IsRunning)
            {
                IsRunning = false;
                OnFinish?.Invoke();                
            }            
        }
    }
}
