using Vector2 = UnityEngine.Vector2;

namespace GS.Asteroids.Core.Interfaces.UIContext
{
    public struct UiGamePlayContext
    {
        public Vector2 PlayerPosition { get; }
        public float PlayerRotation { get; }
        public Vector2 PlayerVelocity { get; }
        public int AlternativeFireCount { get; }
        public float AlternativeFireCoolDown { get; }

        public UiGamePlayContext(
            Vector2 playerPosition,
            float playerRotation,
            Vector2 playerVelocity,
            int alternativeFireCount,
            float alternativeFireCoolDown)
        {
            PlayerPosition = playerPosition;
            PlayerRotation = playerRotation;
            PlayerVelocity = playerVelocity;
            AlternativeFireCount = alternativeFireCount;
            AlternativeFireCoolDown = alternativeFireCoolDown;
        }
    }
}
