namespace GS.Asteroids.Core.Entity
{
    internal interface IRotatable : IEntity
    {
        float Rotation { get; set; }
        float AngularAcceleration { get; }
    }
}
