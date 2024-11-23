using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame{
public class Grid{
    private int Height { get; set; }
    private int Width { get; set; }
    private int TileSize { get; set; }
    private Texture2D Sprite { get; set; }
    private Dictionary<String, Texture2D> Textures { get; set; }
    private ObjetoPosicionado[] Objects { get; set; }
    int index = 0;
    public Grid(int height, int width, int tileSize, Texture2D sprite, Dictionary<Vector2, int> loadedMap, Dictionary<String, Texture2D> textures)
    {
        Height = height;
        Width = width;
        TileSize = tileSize;
        Sprite = sprite;
        Objects = new ObjetoPosicionado[512];
        Textures = textures;
        foreach(var tile in loadedMap){
            
        }
        for(int i = 0; i < width; i++){
            Objects[index++] = new Obstaculo(tileSize, new Vector2(i, -1), textures["obstacleUnbreakable"], false, this, index);
            Objects[index++] = new Obstaculo(tileSize, new Vector2(i, height), textures["obstacleUnbreakable"], false, this, index);
        }
        for(int i = 0; i < height; i++){
            Objects[index++] = new Obstaculo(tileSize, new Vector2(-1, i), textures["obstacleUnbreakable"], false, this, index);
            Objects[index++] = new Obstaculo(tileSize, new Vector2(width, i), textures["obstacleUnbreakable"], false, this, index);
        }
    }
    public void AddObject(ObjetoPosicionado obj){
        Objects[index++] = obj;
    }
    public void AddObject(int id, Vector2 posicion){
        switch(id){
            case 0:
                break;
            case 1:
                Obstaculo obstacleUnbreakable = new Obstaculo(TileSize, posicion, Textures["obstacleUnbreakable"], false, this, index);
                Objects[index++] = obstacleUnbreakable;
                break;
            case 2:
                Obstaculo obstacleBreakable = new Obstaculo(TileSize, posicion, Textures["obstacleUnbreakable"], false, this, index);
                Objects[index++] = obstacleBreakable;
                break;
            case 3:
                Bomba bomb = new Bomba(TileSize, posicion, Textures["obstacleUnbreakable"], this, 2000, index);
                Objects[index++] = bomb;
                break;
            case 4:
                Debug.WriteLine(index);
                Explosion explosion = new Explosion(TileSize, posicion, Textures["obstacleUnbreakable"], this, 1000, index);
                Objects[index++] = explosion;
                break;
        }
    }
    public void DeleteObject(int id){
        Objects[id] = null;
    }
    public void Update(GameTime gameTime, Jugador pj){
        foreach(var obj in Objects){
            if(obj == null){continue;}
            obj.Update(gameTime, pj);
        }

    }
    public void Draw(SpriteBatch spriteBatch){
        spriteBatch.Draw(Sprite, new Rectangle(0, 0, TileSize*Width, TileSize*Height), Color.White);
        foreach(var obj in Objects){
            if (obj == null) {continue;}
            obj.Draw(spriteBatch);
        }
    }
}
}
//array con objetos posicionados y metodo que checkee si hay un objeto en tal posición 
//metodo en objeto posicionado que diga qué tipo de elemnto es (devuelve false por default para todos, usar polimorfismo para cada clase)