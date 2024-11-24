using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame{
public class Obstaculo : ObjetoPosicionado{
    protected bool Destructible;
    public Obstaculo(int tileSize, Vector2 gridPosicion, Texture2D sprite, bool destructible, Grid grilla, int id) : base(tileSize, gridPosicion, sprite, grilla, id)
    {
        Destructible = destructible;
    }
    public void checkColision(GameTime gameTime, Jugador pj){
        Rectangle newHitBox = new Rectangle(pj.hitbox().X + (int)pj._velocidad.X, pj.hitbox().Y + (int)pj._velocidad.Y, this.TileSize, this.TileSize);
        if(this.hitbox().Intersects(newHitBox)){
            pj.bump();
        }
    }
        public override void ThyEndIsNow()
        {
            if(Destructible){
                base.ThyEndIsNow();
            }
        }
        public override void Update(GameTime gameTime, Jugador pj){
        checkColision(gameTime, pj);
    }
}
}