using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame{
public class Explosion : ObjetoPosicionado{
    private double Delay;
    public Explosion(int tileSize, Vector2 gridPosicion, Texture2D sprite, Grid grilla, double delay, int id) : base(tileSize, gridPosicion, sprite, grilla, id)
    {
        Delay = delay;
    }
    
    double timePassed = 0;
    public void explotar(GameTime gameTime, Jugador pj){
        //dañar jugador
        if(this.hitbox().Intersects(pj.hitbox())){
            pj.damage(gameTime);
        }

        //explotar otros objetos
        timePassed += gameTime.ElapsedGameTime.TotalMilliseconds;
        if(timePassed >= Delay){
            Grilla.ExplodeObject(ID);
        }

    }
    public override void Update(GameTime gameTime, Jugador pj){
        explotar(gameTime, pj);
    }
}
}