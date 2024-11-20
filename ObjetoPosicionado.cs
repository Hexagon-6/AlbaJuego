//Esta clase engloba todas las entidades que interactuan con la cuadrícula del mapa
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame{
    public class ObjetoPosicionado{
        protected int TileSize { get; set; }
        public Vector2 GridPosicion { get; set; }
        protected Vector2 AnimationOffset { get; set; }
        public Texture2D Sprite { get; set; }

        public ObjetoPosicionado(int tileSize, Vector2 gridPosicion, Texture2D sprite){
            TileSize = tileSize;
            Sprite = sprite;
            GridPosicion = gridPosicion;
            AnimationOffset = new Vector2(0, 0);
        }
        public Rectangle hitbox(){
            int x = (int)(GridPosicion.X * TileSize);
            int y = (int)(GridPosicion.Y * TileSize);
            return new Rectangle(x, y, TileSize, TileSize);
        }
        public void Draw(SpriteBatch spriteBatch){
            int x = (int)(GridPosicion.X * TileSize) + (int)AnimationOffset.X;
            int y = (int)(GridPosicion.Y * TileSize) + (int)AnimationOffset.Y;
            spriteBatch.Draw(Sprite, new Rectangle(x, y, TileSize, TileSize), Color.White);
        }
        public virtual void Update(GameTime gameTime, Jugador pj){}
    }
}