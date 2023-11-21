namespace GS.Asteroids.Core.Interfaces.UIContext
{
    public struct UiInfoContext
    {
        public string Title { get; }
        public string Description { get; }

        public UiInfoContext(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
