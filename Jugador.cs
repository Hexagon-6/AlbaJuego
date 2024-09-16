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

    bool moviendo = false;
    double timeSinceLastMove = 0;
    Vector2 velocidad = new Vector2(0, 0);
    float delay = 300;
    int steps = 6;
    int stepsRemaining = 6;
    public void Mover(GameTime gameTime){
        if(!moviendo){
            if(Keyboard.GetState().IsKeyDown(Keys.W)){
                velocidad += new Vector2(0, -1f/steps);
                moviendo = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A)){
                velocidad += new Vector2(-1f/steps, 0);
                moviendo = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S)){
                velocidad += new Vector2(0, 1f/steps);
                moviendo = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D)){
                velocidad += new Vector2(1f/steps, 0);
                moviendo = true;
            }
        }
        else{
            timeSinceLastMove += gameTime.ElapsedGameTime.TotalMilliseconds;
            if(timeSinceLastMove >= delay/steps){
                this.AnimationOffset += velocidad*this.TileSize;
                timeSinceLastMove = 0;
                stepsRemaining--;
            }
            if(stepsRemaining == 0){
                this.GridPosicion += velocidad * steps;
                this.AnimationOffset = new Vector2(0, 0);
                moviendo = false;
                velocidad = new Vector2(0, 0);
                timeSinceLastMove = 0;
                stepsRemaining = steps;
            }
        }
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