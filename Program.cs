using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      //   string name = Console.ReadLine();
      GameService vendingDungeon = new GameService();
      vendingDungeon.StartGame();
    }
  }
}
