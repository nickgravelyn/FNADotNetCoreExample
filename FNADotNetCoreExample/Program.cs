using System;

namespace FNADotNetCoreExample
{
  class Program
  {
    static void Main(string[] args)
    {
      DllMap.Register(typeof(Microsoft.Xna.Framework.Vector2).Assembly);

      using (var game = new ExampleGame())
      {
        game.Run();
      }
    }
  }
}
