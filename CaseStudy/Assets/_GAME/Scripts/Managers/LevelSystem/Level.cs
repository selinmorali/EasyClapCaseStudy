using UnityEngine;

namespace _GAME.Scripts.Managers.LevelSystem
{
    public class Level : MonoBehaviour
    {
        public enum State
        {
            Loading,
            Started,
            Succeed,
            Failed
        }

        public enum Stage
        {
            Idle,
            Runner,
            MiniGame,
            Final,
            IsFinished
        }

        public State state;
        public Stage stage;
        internal bool IsFinished => state is State.Failed or State.Succeed;

        private void Start()
        {
            stage = Stage.Idle;
            state = State.Loading;
        }

        private void OnEnable()
        {
            EventManager.OnFirstClick.AddListener(() =>
            {
                state = State.Started;
                stage = Stage.Runner;
            });
            
            EventManager.OnFinalArea.AddListener(FinalStage);
            EventManager.OnLevelSuccess.AddListener(SuccessState);
        }

        private void OnDisable()
        {
            EventManager.OnFirstClick.RemoveListener(() =>
            {
                state = State.Started;
                stage = Stage.Runner;
            });
            EventManager.OnFinalArea.RemoveListener(FinalStage);
            EventManager.OnLevelSuccess.RemoveListener(SuccessState);
        }

        private void SuccessState()
        {
            state = State.Succeed;
        }

        private void FinalStage()
        {
            stage = Stage.Final;
        }
    }
}