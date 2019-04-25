using System;
namespace FragmentLibrary.Domain
{
    public class TilePanel
    {
        public TilePanel(string title)
        {
            Title = title;
        }

        public TilePanel(int id, string title) : this(title)
        {
            Id = id;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

    }
}
