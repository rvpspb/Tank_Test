
using System;
using tank.factory;
using npg.bindlessdi;
using SimplePool;

namespace tank.core
{
    public class Game
    {        
        private Level _currentLevel;        
        private readonly LevelFactory _levelFactory;
        
        public event Action OnPlayerDie;

        public Game(LevelFactory levelFactory)
        {
            _levelFactory = levelFactory;            
        }

        public void LoadLevel()
        {
            _currentLevel = _levelFactory.GetNewInstance();
            Container.Initialize().BindInstance(_currentLevel);
            _currentLevel.Construct();
        }

        public void StartGame()
        {
            _currentLevel.StartSpawn();
        }

        public void EndGame()
        {            
            _currentLevel.ClearSpawn();
            KhtPool.ReturnObject(_currentLevel.gameObject);            
        }        
    }
}
