using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame{
public class Bomba : ObjetoPosicionado{
    protected double Delay;
    private const int EXPLOSION = 4;
    public Bomba(int tileSize, Vector2 gridPosicion, Texture2D sprite, Grid grilla, double delay, int id) : base(tileSize, gridPosicion, sprite, grilla, id)
    {
        Delay = delay;
    }
    
    double timePassed = 0;
    public void explotar(GameTime gameTime){
        timePassed += gameTime.ElapsedGameTime.TotalMilliseconds;
        if(timePassed >= Delay){
            Grilla.AddObject(EXPLOSION, this.GridPosicion);
            Grilla.AddObject(EXPLOSION, this.GridPosicion + new Vector2(1, 0));
            Grilla.AddObject(EXPLOSION, this.GridPosicion + new Vector2(-1, 0));
            Grilla.AddObject(EXPLOSION, this.GridPosicion + new Vector2(0, 1));
            Grilla.AddObject(EXPLOSION, this.GridPosicion + new Vector2(0, -1));
            this.ThyEndIsNow();
        }
    }
    public override void Update(GameTime gameTime, Jugador pj){
        explotar(gameTime);
    }
}
}