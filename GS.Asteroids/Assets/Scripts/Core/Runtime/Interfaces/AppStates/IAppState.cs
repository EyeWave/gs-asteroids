using GS.Asteroids.Core.States;

namespace GS.Asteroids.Core.Interfaces.AppStates
{
    internal interface IAppState
    {
        AppState State { get; }

        void Enter();

        void Exit();

        bool MoveNext();
    }
}
