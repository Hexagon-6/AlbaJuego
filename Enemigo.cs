using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame{
public class Enemigo : ObjetoPosicionado{
    protected int _vidas;
    public Vector2 _velocidad = new Vector2(0, 0);
    public Enemigo(int tileSize, Vector2 gridPosicion, Texture2D sprite, int vidas, Grid grilla, int id) : base(tileSize, gridPosicion, sprite, grilla, id)
    {   
        _vidas = vidas;
    }

    bool moviendo               = false;
    double timeSinceLastStep    = 0;
    double timeSinceLastMove    = 0;
    float moveDelay             = 300;
    float moveCooldown          = 1000;
    int steps                   = 6;
    int stepsRemaining          = 6;
    public void Mover(GameTime gameTime, Jugador pj){
        if(!moviendo){
            timeSinceLastMove += gameTime.ElapsedGameTime.TotalMilliseconds;
            float distanciaJugadorX = pj.GridPosicion.X - this.GridPosicion.X;
            float distanciaJugadorY = pj.GridPosicion.Y - this.GridPosicion.Y;
            if(Math.Abs(distanciaJugadorX) > Math.Abs(distanciaJugadorY)){
                _velocidad = new Vector2(distanciaJugadorX/Math.Abs(distanciaJugadorX), 0);
            }
            else if(Math.Abs(distanciaJugadorY) > Math.Abs(distanciaJugadorX)){
                _velocidad = new Vector2(0, distanciaJugadorY/Math.Abs(distanciaJugadorY));
            }
            else if(distanciaJugadorX == 0 && distanciaJugadorY == 0){
                _velocidad = new Vector2(0, 0);
            }
            else{
                _velocidad = new Vector2(distanciaJugadorX/Math.Abs(distanciaJugadorX), distanciaJugadorY/Math.Abs(distanciaJugadorY));
            }
            if(timeSinceLastMove >= moveCooldown) moviendo = true;
        }
        else{
            timeSinceLastStep += gameTime.ElapsedGameTime.TotalMilliseconds;
            if(timeSinceLastStep >= moveDelay/steps){
                this.AnimationOffset += (_velocidad/steps) * this.TileSize;
                timeSinceLastStep = 0;
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
        moviendo = false;
        _velocidad = new Vector2(0, 0);
        timeSinceLastStep = 0;
        timeSinceLastMove = 0;
        stepsRemaining = steps;
    }


    public void damage(GameTime gameTime){
        if(_vidas > 0){
                _vidas--;
        }
    }

    public void Atacar(GameTime gameTime, Jugador pj){
        if(this.hitbox().Intersects(pj.hitbox())){
            pj.damage(gameTime);
        }
    }

    public override void Update(GameTime gameTime, Jugador pj){
        Mover(gameTime, pj);
        Atacar(gameTime, pj);
    }
}
}