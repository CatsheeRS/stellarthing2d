using Godot;
using System;
using System.Collections.Generic;

namespace spacegame;

public partial class MultiplayerController : Node
{
	[Export]
	public int Port { get; set; } = 0;
	[Export]
	public string Address { get; set; } = "";
	[Export]
	public PackedScene PlayerScene { get; set; }
	[Export]
	public PackedScene UniverseScene { get; set; }
	ENetMultiplayerPeer peer = new();
	List<Player> players = [];

    public override void _Ready()
    {
		
    }

    public void OnHost()
	{
		peer.CreateServer(Port);
		Multiplayer.MultiplayerPeer = peer;
		Multiplayer.PeerConnected += AddPlayer;
		AddPlayer(1); // so the owner joins
	}

    void AddPlayer(long id)
    {
        var player = PlayerScene.Instantiate<Player>();
		player.Username.Text = id.ToString();
		players.Add(player);
    }

    public void OnJoin()
	{
		peer.CreateClient(Address, Port);
		Multiplayer.MultiplayerPeer = peer;
	}

	public void OnStart()
	{
		// Is that a tally hall reference? Once in class, the philosophy teacher said: "our mind is like a labyrinth" i immediately screamed "IS THAT A TALLY HALL REFERENCE?" the class started laughing at me for some reason. It was true, it was a tally hall reference, wasn't it? Another time i was in geography class and she said "today we are gonna learn about storms and springs!" I aggressively said "is that a tally hall reference? And everyone started laughing at me for no reason. It was true. Another time i was in history class and the teacher wanted to show us a quick video, so she asked "turn the lights off, please!" I immediately asked: "is that a tally hall reference?" I was sent to the principal for some reason. But it was true. I had a biology lesson today and the teacher said “ok class today we are learning about neurons”  IS THAT A MOTHERFUCKING MIND ELECTRIC REFERENCE!!!!! I instantly started screaming “AXON DENDRITE AXON DENDRITE”, the whole class was looking at me and the teacher asked me to step outside, I refused and they had to carry me away (mind electric reference!!!). Once I was in the hall I screamed as loud as I could (MIND ELECTRIC REFERENCE!!?? “screeching in the hall of lull”). The teacher asked what I was doing and I said “may I explain my brain has claimed its glory over me” to which he replied “well it looks like you have a thirst for trouble” mind electronic reference! We walked to the principals office and he sat me in a chair (just like an electric chair from the mind electric!!!!!). In his office he told me to tell the whole truth and kept pacing backwards and then forwards (like the mind electric does). Eventually I got bored of his questions and shouted “see how I fly away” (referencing demo 4) and jumped out of the window before falling into a hole I couldn’t see.
		var aloneAtTheEdgeOfTheUniverseHummingATune = UniverseScene.Instantiate();

		foreach (var j in players) {
			aloneAtTheEdgeOfTheUniverseHummingATune.AddChild(j);
		}

		AddChild(aloneAtTheEdgeOfTheUniverseHummingATune);
	}
}
