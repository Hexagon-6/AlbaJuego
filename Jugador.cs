using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame{
public class Jugador : ObjetoPosicionado{
    protected int Vidas;
    public Jugador(int width, int height, int tileSize, Vector2 gridPosicion, Texture2D sprite, int vidas) : base(width, height, tileSize, gridPosicion, sprite)
    {   
        Vidas = vidas;
    }
    bool invincible = false;
    double timeSinceHit = 0;
    float iFramesTime = 1000;

    Vector2 velocidad         = new Vector2(0, 0);
    bool moviendo             = false;
    double timeSinceLastMove  = 0;
    double timeSinceLastInput = 0;
    float moveDelay           = 300;
    float inputDelay          = 200;
    int steps                 = 6;
    int stepsRemaining        = 6;
    public void Mover(GameTime gameTime){ //debe actualizar su gridPosicion
        if(!moviendo){
            if(Keyboard.GetState().IsKeyDown(Keys.W)){
                velocidad.Y = -1f/steps;
                timeSinceLastInput += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A)){
                velocidad.X = -1f/steps;
                timeSinceLastInput += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S)){
                velocidad.Y = 1f/steps;
                timeSinceLastInput += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D)){
                velocidad.X = 1f/steps;
                timeSinceLastInput += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(timeSinceLastInput > inputDelay && !velocidadIsZero()) moviendo = true;
        }
        else{
            timeSinceLastMove += gameTime.ElapsedGameTime.TotalMilliseconds;
            if(timeSinceLastMove >= moveDelay/steps){
                this.AnimationOffset += velocidad*this.TileSize;
                timeSinceLastMove = 0;
                stepsRemaining--;
            }
            if(stepsRemaining == 0){
                this.GridPosicion += velocidad * steps;
                this.AnimationOffset = new Vector2(0, 0);
                timeSinceLastInput = 0;
                moviendo = false;
                velocidad = new Vector2(0, 0);
                timeSinceLastMove = 0;
                stepsRemaining = steps;
            }
        }
    }
    public bool velocidadIsZero(){
        return velocidad.X == 0 && velocidad.Y == 0;
    }
    public void damage(GameTime gameTime){
        if(!invincible && Vidas > 0){
                Vidas--;
                Debug.WriteLine("Vidas: " + this.Vidas);
                Debug.WriteLine("Recibido daño");
                invincible = true;
                if(Vidas == 0){
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
    }
}
}