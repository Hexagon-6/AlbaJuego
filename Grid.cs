using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame{
public class Grid{
    private int Height { get; set; }
    private int Width { get; set; }
    private int TileSize { get; set; }
    private Texture2D Sprite { get; set; }
    public Grid(int height, int width, int tileSize, Texture2D sprite)
    {
        Height = height;
        Width = width;
        TileSize = tileSize;
        Sprite = sprite;
    }
    public void Draw(SpriteBatch spriteBatch){
        spriteBatch.Draw(Sprite, new Rectangle(0, 0, TileSize*Width, TileSize*Height), Color.White);
    }
}
}