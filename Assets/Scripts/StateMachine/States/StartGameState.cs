using npg.states.Infrastructure;
using npg.bindlessdi.UnityLayer;
using tank.input;
using tank.ui;

namespace tank.states
{
	public class StartGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly UnityObjectContainer _unityObjectContainer;		
		private readonly IInput _input;
		private StartPanel _startPanel;

		public StartGameState(GameStateMachine gameStateMachine, UnityObjectContainer unityObjectContainer, IInput input)
		{
			_gameStateMachine = gameStateMachine;
			_unityObjectContainer = unityObjectContainer;			
			_input = input;
		}

		public void Enter()
		{
			if (!_unityObjectContainer.TryGetObject(out _startPanel))
			{
				return;
			}

			_input.SetEnabled(true);
			_startPanel.Show();			

			_input.OnAnyKey += StartGame;
		}

		public void Exit()
		{
			_startPanel.Hide();

			_input.OnAnyKey -= StartGame;
		}

		private void StartGame()
        {
			_gameStateMachine.Enter<CoreGameState>();
        }		
	}
}