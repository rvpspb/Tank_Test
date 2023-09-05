using UnityEngine;

namespace tank.ui
{
    public class PlayPanel : Panel
    {
        [SerializeField] private ScoreView _leftScore;
        [SerializeField] private ScoreView _rightScore;

        public void SetScore(PaddleSide paddleSide, int score)
        {
            switch (paddleSide)
            {
                case PaddleSide.Left: _leftScore.SetScore(score); break;
                case PaddleSide.Right: _rightScore.SetScore(score); break;
            }
        }

        public void ClearScore()
        {
            _leftScore.SetScore(0);
            _rightScore.SetScore(0);
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
