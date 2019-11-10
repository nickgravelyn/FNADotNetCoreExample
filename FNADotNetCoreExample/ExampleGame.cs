using System;
using System.Runtime.InteropServices;
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

      /*
       * In this example we're using the ContentManager to load assets but you
       * could load your content any way you want. In either situation you'd
       * want to use this type of logic to properly setup the paths to your
       * content.
       *
       * Note that there is a gap here whereby a Release build that isn't
       * published won't find content properly. I don't know how important that
       * is for you, so you might need to adjust this a little bit for that type
       * of situation.
       */
#if DEBUG
      /*
       * In debug builds, we load content directly from our Content folder in
       * the root of the repository. This just saves on iteration because you
       * don't have to bother copying files around while developing the game.
       *
       * This is probably not the most stable solution as it's making
       * assumptions about the bin depth produced by .NET Core, but since it's
       * just for debug builds I'm not super worried about having to adjust it
       * with new versions of .NET Core.
       */
      Content.RootDirectory = "../../../../Content";
#else
      /*
       * Windows and Linux simply get a content directory in the executable
       * directory on publish, but macOS requires that we put our content into
       * the Resources folder so we need to adjust the RootDirectory to account
       * for that.
       */
      if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
      {
        Content.RootDirectory = "../Resources/content";
      }
      else
      {
        Content.RootDirectory = "content";
      }
#endif
    }

    protected override void LoadContent()
    {
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
