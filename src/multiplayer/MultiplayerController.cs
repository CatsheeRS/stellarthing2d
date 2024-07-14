using Godot;
using System;

namespace spacegame;

public partial class MultiplayerController : Node
{
	[Export]
	public int Port { get; set; } = 0;
	[Export]
	public string Address { get; set; } = "";
	ENetMultiplayerPeer peer;

    public override void _Ready()
    {
        Multiplayer.PeerConnected += PeerConnected;
		Multiplayer.PeerDisconnected += PeerDisconnected;
		Multiplayer.ConnectedToServer += ConnectedToServer;
		Multiplayer.ConnectionFailed += ConnectionFailed;
    }

	// runs on all peers
    public void PeerConnected(long id)
	{
		GD.Print("peer connected", id);
	}

	// runs on all peers
	public void PeerDisconnected(long id)
	{
		GD.Print("peer disconnected", id);
	}

	// runs on client
	public void ConnectedToServer()
	{
		GD.Print("connected to server");
	}

	// runs on client
	public void ConnectionFailed()
	{
		GD.Print("connection fialed");
	}

    public void OnHost()
	{
		peer = new ENetMultiplayerPeer();
		Error e = peer.CreateServer(Port);
		if (e != Error.Ok) {
			GD.Print(e);
			return;
		}

		// use rangecoder for small stuff and fastlz for big stuff, or at least that's what the tutorial said
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);

		// connect to the server:)
		Multiplayer.MultiplayerPeer = peer;

		GD.Print("waiting for players");
	}

	public void OnJoin()
	{
		peer = new ENetMultiplayerPeer();
		Error e = peer.CreateClient(Address, Port);
		if (e != Error.Ok) {
			GD.Print(e);
			return;
		}

		// use rangecoder for small stuff and fastlz for big stuff, or at least that's what the tutorial said
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);

		// connect to the server:)
		Multiplayer.MultiplayerPeer = peer;

		GD.Print("joining!!!!1");
	}

	public void OnStart()
	{
		
	}
}
