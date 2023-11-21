using System;

namespace GS.Asteroids.Core.Interfaces.GamePlay
{
    internal interface IResultProvider
    {
        int Result { get; }

        void AddReward(int value);

        void Clear();
    }
}
