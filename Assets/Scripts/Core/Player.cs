using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePool;
using System;
using tank.config;
using tank.input;

namespace tank.core
{
    public class Player : Unit
    {
        //public UnitView View { get; private set; }

        private IInput _input;

        private UnitMover _unitMover;

        public Player(PlayerConfig playerConfig, IInput input)
        {
            _gameObject = KhtPool.GetObject(playerConfig.Prefab);
            //View = view.GetComponent<PlayerView>();
            //View.Constuct(unitConfig);
            _input = input;
            _input.OnUpdate += GetInput;


            _unitMover = _gameObject.GetComponent<UnitMover>();
            _unitMover.Construct();


        }

        private void GetInput()
        {
            _unitMover.SetMoveDirection(_input.Vertical);
            _unitMover.SetRotateDirection(_input.Horizontal);
        }

        ~Player()
        {
            _input.OnUpdate -= GetInput;
        }
    }
}
