using System;
using UnityEngine;

namespace _GAME.Scripts.Managers.LevelSystem
{
    public class Level : MonoBehaviour
    {
        public enum State
        {
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
        }

        private void OnEnable()
        {
            EventManager.OnFirstClick.AddListener((() =>
            {
                state = State.Started;
                stage = Stage.Runner;
            }));
        }
    }
}