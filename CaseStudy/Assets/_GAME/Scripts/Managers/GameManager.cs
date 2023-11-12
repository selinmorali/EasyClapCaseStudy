
namespace _GAME.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public void StartGame()
        {
            EventManager.OnFirstClick.Invoke();
        }
    }
}