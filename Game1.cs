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

    private List<Rectangle> textureStore;
    private Dictionary<Vector2, int> tilemap;

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
        Debug.Write('A');
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
