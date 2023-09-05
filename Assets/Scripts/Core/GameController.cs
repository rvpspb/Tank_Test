using System.Collections.Generic;
using System;
using tank.config;
using tank.factory;
using tank.data;
using npg.bindlessdi;

namespace tank.core
{
    public class GameController
    {
        
        private Level _currentLevel;
        private readonly GameConfig _gameConfig;        
        private readonly input.IInput _input;
        private PlayerDataHandler _leftPlayerData;
        private PlayerDataHandler _rightPlayerData;
        private readonly LevelFactory _levelFactory;
        

        public event Action<PaddleSide, int> OnScore;

        public GameController(LevelFactory levelFactory, GameConfig gameConfig, input.IInput input)
        {
            _levelFactory = levelFactory;
            _gameConfig = gameConfig;
            
            _input = input;           

            
            _leftPlayerData = new PlayerDataHandler();
            _rightPlayerData = new PlayerDataHandler();            
        }

        public void LoadLevel()
        {
            //_currentLevel = _levelFactory.Spawn();
            //_currentLevel.Construct();
            //

            Container container = Container.Initialize();

            Player player = container.Resolve<Player>(npg.bindlessdi.Instantiation.InstantiationPolicy.Single);
        }

        public void ResetLevel()
        {
            
            _leftPlayerData.SetScore(0);
            _rightPlayerData.SetScore(0);

            
        }

        

        public void StartGame()
        {           
                    
        }

        public void StopGame()
        {
            
        }

        public void EndGame()
        {
            
        }

        
    }
}

public enum PaddleSide
{
    Left,
    Right,
    None
}
