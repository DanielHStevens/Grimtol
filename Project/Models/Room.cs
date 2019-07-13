using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    // public Dictionary<string, IRoom> Next { get; private set; }

    // public Dictionary<string, IRoom> Previous { get; private set; }
    public string Oracle { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public Dictionary<Item, KeyValuePair<string, IRoom>> LockedExits { get; set; }
    //END props // START methods
    public virtual void PrintDescription()
    {
      System.Console.WriteLine($@"You are in the {Name}.
      {Description}");
    }
    public void AddRoom(string dir, Room room)
    {
      Exits.Add(dir, room);
    }
    public void AddRoom(string dir, Room room, Item access)
    {
      LockedExits.Add(access, new KeyValuePair<string, IRoom>(dir, room));
    }
    public Room Go(string path)
    {
      if (Exits.ContainsKey(path))
      {
        return (Room)Exits[path];
      }
      else
      {
        System.Console.WriteLine("You can not get there.");
        return this;
      }
    }
    public void AddItem(Item item)
    {
      Items.Add(item);
    }

    //START Ctor constructor
    public Room(string name, string description, string oracle)
    {
      Name = name;
      Description = description;
      Oracle = oracle;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
      LockedExits = new Dictionary<Item, KeyValuePair<string, IRoom>>();
    }

  }
}