/*
TODO jugador, grilla, texturas en global
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private int _tileSize = 50; 
    private int _mapWidth = 16;
    private int _mapHeight = 9;
    /*
    50 - 16 - 9 para windowed default

    120 - 16 - 9 para 1920x1080
    */

    //private List<Rectangle> textureStore;
    private Dictionary<Vector2, int> tilemap;

    Grid mapa;
    Jugador pj;
    Proyectil1 proy; //<-- temp+

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public Dictionary<Vector2, int> LoadMap(String path){
        Dictionary<Vector2, int> result = new Dictionary<Vector2, int>();
        using(StreamReader csvFile = new StreamReader(path)){
            string line; 
            int y = 0;
            while((line = csvFile.ReadLine()) != null){
                int x = 0;
                int[] row = line.Split(',').Select(num => Convert.ToInt32(num)).ToArray();
                foreach(int val in row){
                    result[new Vector2(x, y)] = val; 
                    x++;
                }
                y++;
            }
        }
        return result;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        // _graphics.IsFullScreen = true;
        // _graphics.ApplyChanges();
        //Debug.Write('A');
    }

    protected override void LoadContent()
    {
        
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Dictionary<String, Texture2D> _textures = new Dictionary<string, Texture2D>{
            {"pj", Content.Load<Texture2D>("sprites/pj")},
            {"mapa", Content.Load<Texture2D>("fondos/fondolv1")},
            {"proyectil", Content.Load<Texture2D>("sprites/proy1")},
            {"obstacleUnbreakable", Content.Load<Texture2D>("sprites/proy1")}
        };

        String mapa_tilemap_path = "Data/map.csv";
        mapa = new Grid(_mapHeight, _mapWidth, _tileSize, _textures["mapa"], LoadMap(mapa_tilemap_path), _textures); 

        pj = new Jugador(_tileSize, new Vector2(6, 2), _textures["pj"], 5, mapa, -1);


        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        pj.Update(gameTime);
        mapa.Update(gameTime, pj);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState : SamplerState.PointClamp);
        mapa.Draw(_spriteBatch);
        pj.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
