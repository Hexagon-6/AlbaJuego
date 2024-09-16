//Esta clase engloba todas las entidades que interactuan con la cuadrícula del mapa
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame{
    public class ObjetoPosicionado{
        protected int Width { get; set; }
        protected int Height { get; set; }
        protected int TileSize { get; set; }
        protected Vector2 GridPosicion { get; set; }
        protected Vector2 AnimationOffset { get; set; }
        protected Texture2D Sprite { get; set; }

        public ObjetoPosicionado(int width, int height, int tileSize, Vector2 gridPosicion, Texture2D sprite){
            Width = width;
            Height = height;
            TileSize = tileSize;
            Sprite = sprite;
            GridPosicion = gridPosicion;
            AnimationOffset = new Vector2(0, 0);
        }
        public Rectangle hitbox(){
            int x = (int)(GridPosicion.X * TileSize);
            int y = (int)(GridPosicion.Y * TileSize);
            return new Rectangle(x, y, Width, Height);
        }
        public void Draw(SpriteBatch spriteBatch){
            int x = (int)(GridPosicion.X * TileSize) + (int)AnimationOffset.X;
            int y = (int)(GridPosicion.Y * TileSize) + (int)AnimationOffset.Y;
            spriteBatch.Draw(Sprite, new Rectangle(x, y, Width, Height), Color.White);
        }
    }
}