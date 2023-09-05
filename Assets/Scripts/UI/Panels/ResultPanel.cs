using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace tank.ui
{
    public class ResultPanel : Panel
    {
        [SerializeField] private TMP_Text _resultText;

        private Dictionary<PaddleSide, string> _playerNames = new() { { PaddleSide.Left, "Player 1" }, { PaddleSide.Right, "Player 2" }, { PaddleSide.None, "Nobody" } };
                
        public void SetWinner(PaddleSide paddleSide)
        {
            string result = $"{_playerNames[paddleSide]} Wins!";
            _resultText.SetText(result);
        }
                
        protected override void OnShow()
        {
            base.OnShow();
        }

        protected override void OnHide()
        {
            base.OnHide();
        }
    }
}
