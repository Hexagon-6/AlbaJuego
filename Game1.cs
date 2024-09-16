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

    //private List<Rectangle> textureStore;
    private Dictionary<Vector2, int> tilemap;

    Grid mapa;
    Jugador pj;
    Proyectil1 proy; //<-- temp

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
        //Debug.Write('A');
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        String mapa_tex_path = "fondos/fondolv1";
        Texture2D mapa_tex = Content.Load<Texture2D>(mapa_tex_path);
        mapa = new Grid(8, 8, _tileSize, mapa_tex); 

        String pj_tex_path = "sprites/pj";
        Texture2D pj_tex = Content.Load<Texture2D>(pj_tex_path);
        pj = new Jugador(_tileSize, _tileSize, _tileSize, new Vector2(6, 2), pj_tex, 5);

        String proy_tex_path = "sprites/proy1";
        Texture2D proy_tex = Content.Load<Texture2D>(proy_tex_path);
        proy = new Proyectil1(_tileSize, _tileSize, _tileSize, new Vector2(2, 2), proy_tex);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        pj.Update(gameTime);
        proy.Update(gameTime, pj);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState : SamplerState.PointClamp);
        mapa.Draw(_spriteBatch);
        pj.Draw(_spriteBatch);
        proy.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
