using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame{
public class Spawner : ObjetoPosicionado{
    protected double TimeAlive;
    protected double Delay;
    private int enemiesSpawned = 0;
    private const int ENEMIGO = 5;
    public Spawner(int tileSize, Vector2 gridPosicion, Texture2D sprite, Grid grilla, double delay, double timeAlive, int id) : base(tileSize, gridPosicion, sprite, grilla, id)
    {
        TimeAlive = timeAlive;
        Delay = delay;
    }
    
    double timePassed = 0;
    public void spawnear(GameTime gameTime){
        timePassed += gameTime.ElapsedGameTime.TotalMilliseconds;
        if(timePassed >= Delay * enemiesSpawned + 1){
            Grilla.AddObject(ENEMIGO, this.GridPosicion);
            enemiesSpawned++;
        }
        if(timePassed >= TimeAlive){
            Grilla.DeleteObject(ID);
        }
    }
        public override void ThyEndIsNow()
        {
            
        }
        public override void Update(GameTime gameTime, Jugador pj){
        spawnear(gameTime);
    }
}
}