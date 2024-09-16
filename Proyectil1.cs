using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame{
public class Proyectil1 : ObjetoPosicionado{
    public Proyectil1(int width, int height, int tileSize, Vector2 gridPosicion, Texture2D sprite) : base(width, height, tileSize, gridPosicion, sprite)
    {

    }
    public void checkColision(GameTime gameTime, Jugador pj){
        if(this.hitbox().Intersects(pj.hitbox())){
            pj.damage(gameTime);
        }
    }
    public void Update(GameTime gameTime, Jugador pj){
        checkColision(gameTime, pj);
    }
}
}