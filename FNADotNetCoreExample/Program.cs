using System;
using Microsoft.Xna.Framework;

namespace FNADotNetCoreExample
{
  class ExampleGame : Game
  {
    public ExampleGame()
    {
      new GraphicsDeviceManager(this)
      {
        PreferredBackBufferWidth = 960,
        PreferredBackBufferHeight = 540,
      };
    }

    protected override void Draw(GameTime time)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);
    }

    static void Main(string[] args)
    {
      using (var game = new ExampleGame())
        game.Run();
    }
  }
}
