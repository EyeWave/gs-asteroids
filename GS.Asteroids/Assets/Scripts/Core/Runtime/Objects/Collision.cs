using GS.Asteroids.Core.Entity;

namespace GS.Asteroids.Core.Objects
{
    internal readonly struct Collision
    {
        internal ICollidable First { get; }
        internal ICollidable Second { get; }

        internal Collision(ICollidable first) : this(first, second: null)
        {
        }

        internal Collision(ICollidable first, ICollidable second)
        {
            First = first;
            Second = second;
        }
    }
}
