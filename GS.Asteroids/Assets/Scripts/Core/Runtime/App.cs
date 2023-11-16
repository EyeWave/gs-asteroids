using GS.Asteroids.Core.Factories;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.AppStates;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.States;
using System;

namespace GS.Asteroids.Core
{
    public class App
    {
        public App(IAppContext appContext)
        {
            IObjectProvider objectProvider = ObjectProviderFactory.Create(appContext.Level, appContext.AppConfigDataProvider);
            CompositeProvider compositeProvider = new CompositeProvider(appContext.Root, objectProvider);

            Func<AppState, AppState> stateSwitcher = exitState => exitState switch
            {
                AppState.WaitRun => AppState.GamePlay,
                AppState.GamePlay => AppState.GameOver,
                AppState.GameOver => AppState.WaitRun,
                _ => throw new NotImplementedException($"Unknown state {exitState}")
            };

            AppStateMachine stateMachine = new AppStateMachine(stateSwitcher, appContext.Logger);

            IAppState waitRunState = new WaitRunState
            (
                compositeProvider,
                appContext.Level,
                appContext.AppConfigDataProvider,
                appContext.InputSystem,
                appContext.DrawSystem,
                appContext.UiSystem,
                appContext.LocalizationSystem
            );

            IAppState gamePlaySate = new GamePlaySate
            (
                compositeProvider,
                appContext.Level,
                appContext.AppConfigDataProvider,
                appContext.InputSystem,
                appContext.DrawSystem
            );

            IAppState gameOverSate = new GameOverSate
            (
                appContext.InputSystem,
                appContext.UiSystem,
                appContext.LocalizationSystem
            );

            stateMachine.SetStates(waitRunState, gamePlaySate, gameOverSate);
            appContext.Root.Install(stateMachine);
        }
    }
}
