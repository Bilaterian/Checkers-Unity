using UnityEngine;
using System;

[Serializable]
public class Player
{
    public string name;
    public int wins;
    public int losses;
    public int piecesCaptured;

    public Player(string name)
    {
        this.name = name;
    }
}