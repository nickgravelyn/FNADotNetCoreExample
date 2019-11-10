using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FNADotNetCoreExample
{
  class ExampleGame : Game
  {
    private SpriteBatch _spriteBatch;
    private Texture2D _texture;

    public ExampleGame()
    {
      new GraphicsDeviceManager(this)
      {
        PreferredBackBufferWidth = 960,
        PreferredBackBufferHeight = 540,
      };
    }

    protected override void LoadContent()
    {
#if DEBUG
      /*
       * In debug builds, we load content directly from our Content folder in
       * the root of the repository.
       */
      Content.RootDirectory = "../../../../Content";
#endif

      _spriteBatch = new SpriteBatch(GraphicsDevice);
      _texture = Content.Load<Texture2D>("xna");
    }

    protected override void Draw(GameTime time)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      _spriteBatch.Begin();
      _spriteBatch.Draw(_texture, Vector2.Zero, Color.White);
      _spriteBatch.End();
    }
  }
}
