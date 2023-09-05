using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using npg.bindlessdi.UnityLayer;
using tank.ui;

namespace tank.di
{
    public class GameSceneContext : SceneContext
    {
        [SerializeField] private PlayPanel _playPanel;
        [SerializeField] private ResultPanel _resultPanel;
        [SerializeField] private StartPanel _startPanel;

        public override IEnumerable<Object> GetObjects()
        {
            return new Object[] { _playPanel, _resultPanel, _startPanel };
        }
    }
}
