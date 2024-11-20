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
    private ObjetoPosicionado[] Objects {get; set;}
    public Grid(int height, int width, int tileSize, Texture2D sprite, Dictionary<Vector2, int> loadedMap, Dictionary<String, Texture2D> textures)
    {
        Height = height;
        Width = width;
        TileSize = tileSize;
        Sprite = sprite;
        Objects = new ObjetoPosicionado[256];
        int index = 0;
        foreach(var tile in loadedMap){
            switch(tile.Value){
                case 0:
                    break;
                case 1:
                    Obstaculo obstacleUnbreakable = new Obstaculo(tileSize, tile.Key, textures["obstacleUnbreakable"], false);
                    Objects[index++] = obstacleUnbreakable;
                    break;
            }
        }
        for(int i = 0; i < width; i++){
            Objects[index++] = new Obstaculo(tileSize, new Vector2(i, -1), textures["obstacleUnbreakable"], false);
            Objects[index++] = new Obstaculo(tileSize, new Vector2(i, height), textures["obstacleUnbreakable"], false);
        }
        for(int i = 0; i < height; i++){
            Objects[index++] = new Obstaculo(tileSize, new Vector2(-1, i), textures["obstacleUnbreakable"], false);
            Objects[index++] = new Obstaculo(tileSize, new Vector2(width, i), textures["obstacleUnbreakable"], false);
        }
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