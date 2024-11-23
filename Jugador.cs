using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame{
public class Jugador : ObjetoPosicionado{
    protected int _vidas;
    public Vector2 _velocidad = new Vector2(0, 0);
    private const int BOMBA = 3;
    public Jugador(int tileSize, Vector2 gridPosicion, Texture2D sprite, int vidas, Grid grilla, int id) : base(tileSize, gridPosicion, sprite, grilla, id)
    {   
        _vidas = vidas;
    }
    bool invincible = false;
    double timeSinceHit = 0;
    float iFramesTime = 480;

    bool moviendo               = false;
    double timeSinceLastMove    = 0;
    double timeSinceLastInput   = 0;
    float moveDelay             = 300;
    float inputDelay            = 120;
    int steps                   = 6;
    int stepsRemaining          = 6;
    public void Mover(GameTime gameTime){ //debe actualizar su gridPosicion
        if(!moviendo){
            if(Keyboard.GetState().IsKeyDown(Keys.W)){
                _velocidad = new Vector2 (_velocidad.X , -1f);
                timeSinceLastInput += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A)){
                _velocidad = new Vector2(-1f, _velocidad.Y);
                timeSinceLastInput += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S)){
                _velocidad = new Vector2 (_velocidad.X , 1f);
                timeSinceLastInput += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D)){
                _velocidad = new Vector2(1f, _velocidad.Y);
                timeSinceLastInput += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(timeSinceLastInput > inputDelay && !velocidadIsZero()) moviendo = true;
        }
        else{
            timeSinceLastMove += gameTime.ElapsedGameTime.TotalMilliseconds;
            if(timeSinceLastMove >= moveDelay/steps){
                this.AnimationOffset += (_velocidad/steps) * this.TileSize;
                timeSinceLastMove = 0;
                stepsRemaining--;
            }
            if(stepsRemaining == 0){
                this.GridPosicion += _velocidad;
                this.bump();
            }
        }
    }
    public bool velocidadIsZero(){
        return this._velocidad.X == 0 && this._velocidad.Y == 0;
    }
    public void bump(){
        this.AnimationOffset = new Vector2(0, 0);
        timeSinceLastInput = 0;
        moviendo = false;
        _velocidad = new Vector2(0, 0);
        timeSinceLastMove = 0;
        stepsRemaining = steps;
    }

    double timeSinceLastBomb = 0;
    float bombDelay = 1000;
    public void PonerBomba(GameTime gameTime){
        timeSinceLastBomb += gameTime.ElapsedGameTime.TotalMilliseconds;
        if(timeSinceLastBomb >= bombDelay && Keyboard.GetState().IsKeyDown(Keys.N) && !moviendo){ //mover parte del input a movimiento en todo caso idk
            Grilla.AddObject(BOMBA, this.GridPosicion);
            timeSinceLastBomb = 0;
        }  
    }

    public void damage(GameTime gameTime){
        if(!invincible && _vidas > 0){
                _vidas--;
                Debug.WriteLine("Vidas: " + this._vidas);
                Debug.WriteLine("Recibido daño");
                invincible = true;
                if(_vidas == 0){
                    Debug.WriteLine("Game Over!");
                }
        }
        else{
            timeSinceHit += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceHit >= iFramesTime){
                invincible = false;
                timeSinceHit = 0;
            }
        }
    }
    public void Update(GameTime gameTime){
        Mover(gameTime);
        PonerBomba(gameTime);
    }
}
}