using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using npg.bindlessdi;
using tank.states;
using tank.config;
using tank.core;
using tank.factory;
using tank.input;

namespace tank.di
{
    public class EnterPoint : MonoBehaviour
    {
        [SerializeField] private input.KeyboardInput _keyboardInput;
        [SerializeField] private LevelFactory _levelFactory;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private CameraMover _cameraMover;

        [SerializeField] private WeaponConfigSet _weaponConfigSet;
        [SerializeField] private Level _levelPrefab;        

        private IInput _input;

        private void Awake()
        {
            Container container = Container.Initialize();

            

            _input = _keyboardInput;
            container.BindInstance(_input);

            //container.BindImplementation<IInput, KeyboardInput>();
            
            container.BindInstance(_gameConfig);
            container.BindInstance(_playerConfig);
            container.BindInstance(_cameraMover);
            container.BindInstance(_weaponConfigSet);
            container.BindInstance(_levelPrefab);
            //container.BindInstance(_levelFactory);            

            GameStateMachine gameStateMachine = container.Resolve<GameStateMachine>();
            gameStateMachine.Enter<LoadGameState>();
        }
    }
}
