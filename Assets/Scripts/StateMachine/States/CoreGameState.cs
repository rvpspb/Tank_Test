using npg.states.Infrastructure;
using Cysharp.Threading.Tasks;
using npg.bindlessdi.UnityLayer;
using tank.core;
using tank.config;
using tank.ui;
using tank.helpers;

namespace tank.states
{
	public class CoreGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly UnityObjectContainer _unityObjectContainer;
		private readonly GameConfig _gameConfig;
		private readonly GameController _gameController;		
		private PlayPanel _playPanel;
		private GameTimer _gameTimer;

		public CoreGameState(GameStateMachine gameStateMachine, UnityObjectContainer unityObjectContainer, GameConfig gameConfig, GameController gameController)
		{
			_gameStateMachine = gameStateMachine;
			_unityObjectContainer = unityObjectContainer;
			_gameConfig = gameConfig;
			_gameController = gameController;			
		}

		public void Enter()
		{
			if (!_unityObjectContainer.TryGetObject(out _playPanel))
			{
				return;
			}

			_playPanel.Show();
			_playPanel.ClearScore();
			_gameController.StartGame();

			_gameTimer = new GameTimer(_gameConfig.GamePeriod);
			_gameTimer.Start();

			//_gameController.OnScore += OnScore;
			//_gameTimer.OnTargetTime += OnTimer;
		}

		public void Exit()
		{
			//_gameController.OnScore -= OnScore;
			//_gameTimer.OnTargetTime -= OnTimer;

			_gameController.StopGame();			
			_playPanel.Hide();
		}

		private void OnTimer()
        {		
			_gameStateMachine.Enter<EndGameState, PaddleSide>(PaddleSide.None);
		}

		private void OnScore(PaddleSide paddleSide, int score)
        {
			_playPanel.SetScore(paddleSide, score);

			if (score >= _gameConfig.WinScore)
            {
				_gameStateMachine.Enter<EndGameState, PaddleSide>(paddleSide);
				return;
			}

			StopAndResumeGame();
		}

		private async UniTask StopAndResumeGame()
		{
			_gameController.StopGame();
			await UniTask.Delay(1000);
			_gameController.StartGame();
		}			
	}
}