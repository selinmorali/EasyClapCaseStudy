namespace _GAME.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public bool isGameStarted;

        public void StartGame()
        {
            isGameStarted = true;
            EventManager.OnGameStart.Invoke();
        }
    }
}