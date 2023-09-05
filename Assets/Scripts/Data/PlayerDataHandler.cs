
namespace tank.data
{
    public class PlayerDataHandler
    {
        private PlayerData _playerData;

        public int Score => _playerData.Score;

        public PlayerDataHandler()
        {
            _playerData = new PlayerData();
        }

        public void AddScore()
        {
            _playerData.Score++;
        }

        public void SetScore(int value)
        {
            _playerData.Score = value;            
        }
    }
}
