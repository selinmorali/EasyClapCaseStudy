
namespace _GAME.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public bool isFirstClick;

        public void StartGame()
        {
            isFirstClick = true;
            EventManager.OnFirstClick.Invoke();
        }
    }
}