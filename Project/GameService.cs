using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public Player CurrentPlayer { get; set; }

    public Room CurrentRoom { get; set; }

    public void GetUserInput(string input)
    {
      // go use take look inventory help quit planets lines 62-77
      string[] inputs = input.Split(' ');
      string command = inputs[0];
      string option = "";
      if (inputs.Length > 1)
      {
        option = inputs[1];
      }
      switch (command)
      {
        case "go":
          Go(option);
          break;
        case "use":
          UseItem(option);
          break;
      }
    }

    public void Go(string direction)
    // public Dictionary<string, IRoom> Exits { get; set; }
    {
      //   string givenDirection = CurrentRoom.Exits.find(direction);
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = (Room)CurrentRoom.Exits[direction];
      }
      else
      {
        Console.WriteLine("You can not go in that direction.");
      }

    }

    public void Help()
    {

    }

    public void Inventory()
    {

    }

    public void Look()
    {

    }

    public void Quit()
    {

    }

    public void Reset()
    {

    }
    private Room currentRoom;
    private bool exploring = true;
    public void Setup()
    {

      //Rooms
      Room chuteLanding = new Room("chuteLanding", "You are in the dark, dank room with the chute you fell down. Unfortunately it is too steep to climb, but you can see light up at the top, barely. It seems some of the vending machines conents spilled to the ground when the floor opened up beneath you and are now lying on the ground next to you. East of you lies a door to go further in.", "Go East.");
      Room crossRoad = new Room("CrossRoads", "The second room you enter is as dank musky as the last. It has halls going both to the north and south. You can't see far in either direction.", "Either way is fine.");
      Room rubbleRoom = new Room("RubbleRoom", "You are in the dark, dank room with the chute you fell down. Unfortunately it is too steep to climb, but you can see light up at the top, barely. East of you lies a door to go further in.", "Perhaps some help could get you through this.");
      Room wizardStudy = new Room("WizardStudy", "Inside is a surprisingly well lit room. Lit by some sort of lichen or moss on various parts of the room. Shelves of books that look like they0'll fall apart if you touched them line the walls, yet on the table in front of you lies a large black tome.", "Use the magic necromancy tome. That never goes bad.");
      Room puzzleRoom = new Room("PuzzleRoom", @"In this room you see a see a body... or really the remains of a body, skelital and covered in rags. An old sword lies next to it, rusty and dented. It seems like he tried to dig his way out, through the stone. With a sword. It clearly did  not work. On the thick door(to the East) is an inscription. At first you couldnt read it, but it swirls and then before you you see a riddle:
    I am lighter than a feather.
    Anyone can hold me.
    Yet not for long.", "The skull is useful. The answer is breath... just breath on the door.");
      Room runeRoom = new Room("RuneRoom", "This room is covered completly in runes. You would need some help translating it. On the floor, tucked in the corner is a key. To the South is a door further in. West brings you back.", "This room is filler really.");
      Room goblinRoom = new Room("GoblinRoom", "As you enter a goblin leaps up and brandishes... a shovel? Well it swings the shovel at you, screaming.", "Use the skull. or the bees. but the skull is better.");
      Room ropeBridge = new Room("RopeBridge", "You enter this room, and you see a rope bridge spanning from where you came from(west) to the other side of the room(west). There is no floor, only a long drop.", "Use the bridge, what do you think you are supposed to do?");
      Room orcRoom = new Room("OrcRoom", "A smell of rot and decay hits you. Several bodies of both humans and goblins fill this room. A hulking ork stands up from its current... meal and looks at you. Hunger in its eyes. There is no way of getting past it to the door(east).", "Use the bees, or food, or maybe if you have a special ability...");
      Room treasureRoom = new Room("TreasureRoom", "In this room is a chest, sturdy wood and metal as if it pulled from a cliche image of a chest.  To the south is a door, bits of light peeking out from under it.", "Use the key to get into the chest, there's cool stuff in there, then leave. Or you could just leave I guess.");
      chuteLanding.AddRoom("east", crossRoad);
      crossRoad.AddRoom("west", chuteLanding);
      crossRoad.AddRoom("south", rubbleRoom);
      crossRoad.AddRoom("north", puzzleRoom);
      rubbleRoom.AddRoom("north", rubbleRoom);
      puzzleRoom.AddRoom("south", crossRoad);
      runeRoom.AddRoom("east", puzzleRoom);
      goblinRoom.AddRoom("north", runeRoom);
      ropeBridge.AddRoom("west", goblinRoom);
      orcRoom.AddRoom("west", ropeBridge);


      //ITEMS
      Item breath = new Item("breath", "We breath.");
      CurrentPlayer.Inventory.Add(breath);
      Item food = new Item("food", "some food that came out of a... dubious vending machine.");
      chuteLanding.AddItem(food);
      Item skull = new Item("skull", "A human(probably) skull that seems to be completly picked clean of flesh. ...morbid.. .. facinating...");
      puzzleRoom.AddItem(skull);
      Item sword = new Item("sword", "A rusty, dented sword with the end broken off. Better than nothing?");
      puzzleRoom.AddItem(sword);
      Item key = new Item("key", "A very cliche looking skeleton key.");
      runeRoom.AddItem(key);
      Asset goblin = new Asset("Gork The Goblin", "A small hunched figure that wields a shovel and now serves you enthusiasticly.");
      goblinRoom.AddItem(goblin);
      Item bridge = new Item("bridge", "A rope bridge??");
      ropeBridge.AddItem(bridge);
      Item tome = new Item("tome", @"A leather-bound large tome with words printed in silver on the front:
        Memento Mori
        Vicere Mori
    Inside seems to be... notes and instructions and spells for necromancy.");
      wizardStudy.AddItem(tome);
      Asset magic = new Asset("Magic", "Through a brief but informitive study of a black tome you found in a glowing-moss covered room in a dark dungen you have gained a brief but useful understanding of necromantic magics.");
      goblinRoom.AddItem(magic);

      puzzleRoom.AddRoom("west", runeRoom, magic);
      rubbleRoom.AddRoom("east", wizardStudy, goblin);
      runeRoom.AddRoom("south", goblinRoom, breath);
      goblinRoom.AddRoom("east", ropeBridge, goblin);
      goblinRoom.AddRoom("east", ropeBridge, food);
      ropeBridge.AddRoom("east", orcRoom, bridge);
      orcRoom.AddRoom("east", treasureRoom, magic);
      //   orcRoom.AddRoom("east", treasureRoom, bees);

      currentRoom = chuteLanding;
    }

    public void StartGame()
    {
      while (exploring)
      {
        currentRoom.PrintDescription();
        string input = Console.ReadLine().ToLower();
        GetUserInput(input);
      }
    }

    public void TakeItem(string itemName)
    {
      // if is Asset add to assets
    }

    public void UseItem(string itemName)
    {
      Item stuff = CurrentPlayer.Inventory.Find(x => x.Name == itemName);
      if (stuff != null && CurrentRoom.LockedExits.ContainsKey(stuff))
      {
        KeyValuePair<string, IRoom> unlockedRoom = CurrentRoom.LockedExits[stuff];
        CurrentRoom.Exits.Add(unlockedRoom.Key, unlockedRoom.Value);
        CurrentRoom.LockedExits.Remove(stuff);

      }
      // remove from inventory(maybew)
    }

    public void GetUserInput()
    {
      throw new NotImplementedException();
    }

    //START Ctor constructor
    public GameService(Room currentRoom, Player currentPlayer)
    {
      //   Name = name;
      currentRoom = CurrentRoom;
      currentPlayer = CurrentPlayer;
      Setup();
    }
  }
}