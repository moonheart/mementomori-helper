using System.Drawing;

namespace MementoMori.WebUI.UI
{
    public class Sprite
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Color Color { get; set; }
        public string Filter { get; set; }
        public string Url { get; set; }
    }
}
