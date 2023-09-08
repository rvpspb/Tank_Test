using npg.states.Infrastructure;
using npg.bindlessdi.UnityLayer;
using tank.core;
using tank.input;
using tank.ui;

namespace tank.states
{
	public class EndGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly UnityObjectContainer _unityObjectContainer;		
		private readonly Game _game;
		private readonly IInput _input;
		private ResultPanel _resultPanel;

		public EndGameState(GameStateMachine gameStateMachine, UnityObjectContainer unityObjectContainer, Game game, IInput input)
		{
			_gameStateMachine = gameStateMachine;
			_unityObjectContainer = unityObjectContainer;			
			_game = game;
			_input = input;
		}

		public void Enter()
		{
			if (!_unityObjectContainer.TryGetObject(out _resultPanel))
			{
				return;
			}
						
			_resultPanel.Show();

			_input.OnAnyKey += RestartGame;
		}

		public void Exit()
		{
			_input.OnAnyKey -= RestartGame;

			_game.EndGame();
			_resultPanel.Hide();
		}
				
		private void RestartGame()
		{
			_gameStateMachine.Enter<LoadGameState>();
		}
	}
}