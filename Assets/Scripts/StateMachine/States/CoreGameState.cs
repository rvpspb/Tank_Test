using npg.states.Infrastructure;
using Cysharp.Threading.Tasks;
using npg.bindlessdi.UnityLayer;
using npg.bindlessdi;
using tank.core;
using tank.ui;

namespace tank.states
{
	public class CoreGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly UnityObjectContainer _unityObjectContainer;		
		private readonly Game _game;		
		private PlayPanel _playPanel;		
		private Player _player;

		public CoreGameState(GameStateMachine gameStateMachine, UnityObjectContainer unityObjectContainer, Game game)
		{
			_gameStateMachine = gameStateMachine;
			_unityObjectContainer = unityObjectContainer;			
			_game = game;			
		}

		public void Enter()
		{
			if (!_unityObjectContainer.TryGetObject(out _playPanel))
			{
				return;
			}

			_player = Container.Initialize().Resolve<Player>();
			_player.OnDie += OnLose;

			_playPanel.Show();			
			_game.StartGame();
		}

		public void Exit()
		{				
			_playPanel.Hide();
		}

		private void OnLose()
        {
			_player.OnDie -= OnLose;
			WaitAndEnd();
		}

		private async UniTask WaitAndEnd()
		{
			int waitTime = 3000;
			await UniTask.Delay(waitTime);
			_gameStateMachine.Enter<EndGameState>();
		}						
	}
}