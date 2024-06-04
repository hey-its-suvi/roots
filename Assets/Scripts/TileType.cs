using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
// using UnityEditor.UI;
using UnityEngine;


public abstract class TileType
{
    public abstract void init(GameStateManager gameStateManager);
    
    public abstract bool canMoveFrom(Move move);

    public abstract bool canMoveTo(Tile from, Tile to, Move move);

    public abstract TileType doMoveFrom(Tile to, Move move);

    public abstract TileType doMoveTo(Tile From, Move move);

    public abstract Sprite getSprite();



}
public class PathTile : TileType
{

    Move enter;
    Move exit;

    GameStateManager gameStateManager;

    public override void init(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }
    public PathTile(Move enter, Move exit)
    {
        this.enter = enter;
        this.exit = exit;
    }
    public override bool canMoveFrom(Move move)
    {
        return true;
    }
    public override bool canMoveTo(Tile from, Tile to, Move move)
    {
        // If the move is opposite to the exit path of this tile, then backtracking is happening.
        if(exit == Utils.oppositeMove(move))
        {
            return true;
        }

        // if((move == Move.Left && exit==Move.Right) || 
        // (move == Move.Right && exit==Move.Left) ||
        // (move == Move.Up && exit==Move.Down)||
        // (move == Move.Down && exit==Move.Up))
        // {
        //     return true;
        // }
        return false;
    }

    public override TileType doMoveFrom(Tile to, Move move)
    {
        // We are exiting this tile
        if(move == enter)
        {
            return new EmptyTile();
        }
        return new PathTile(enter, move);
    }

    public override TileType doMoveTo(Tile From, Move move)
    {
        // We are backtracking;
        if(exit == Utils.oppositeMove(move))
        {
            gameStateManager.movesLeft++;
            return new PathTile(enter, Move.None);
        }
        return new PathTile(Utils.oppositeMove(move), Move.None);

    }

    public override Sprite getSprite()
    {
        string enterString = Enum.GetName(typeof(Move), enter);
        string exitString = Enum.GetName(typeof(Move), exit);

        string spriteString = "path"+enterString+"To"+exitString;
        return gameStateManager.spriteLoader.getSprite(spriteString);
    }


}

public class EmptyTile: TileType
{
    GameStateManager gameStateManager;

    public override void init(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    // Should never happen
    public override bool canMoveFrom(Move move)
    {
        return false;
    }

    public override bool canMoveTo(Tile from, Tile to, Move move)
    {
        if(gameStateManager.movesLeft == 0) return false;
        else return true;
    }

    // Should never happen
    public override TileType doMoveFrom(Tile to, Move move)
    {
        return new EmptyTile();
    }

    public override TileType doMoveTo(Tile From, Move move)
    {
        gameStateManager.movesLeft--;
        PathTile newPath = new PathTile(Utils.oppositeMove(move), Move.None);
        newPath.init(gameStateManager);
        return newPath;
    }

    public override Sprite getSprite()
    {
        return gameStateManager.spriteLoader.getSprite("base");
    }


}

public class RockTile : TileType
{

    GameStateManager gameStateManager;

    public override void init(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    // Should never happen
    public override bool canMoveFrom(Move move)
    {
        return false;
    }

    public override bool canMoveTo(Tile from, Tile to, Move move)
    {
        return false;
    }

    // Should never happen
    public override TileType doMoveFrom(Tile to, Move move)
    {
        return new RockTile();
    }

    // Should never happen

    public override TileType doMoveTo(Tile From, Move move)
    {
       return new RockTile();
    }

    public override Sprite getSprite()
    {
        return gameStateManager.spriteLoader.getSprite("rock");
    }

}

public class BreakableRockTile : TileType
{

    GameStateManager gameStateManager;
    Move breakDirection;

    public BreakableRockTile(Move breakDirection)
    {
        this.breakDirection = breakDirection;
    }
    public override void init(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    // Should never happen
    public override bool canMoveFrom(Move move)
    {
        return false;
    }

    public override bool canMoveTo(Tile from, Tile to, Move move)
    {
        if(move == Utils.oppositeMove(breakDirection) && gameStateManager.movesLeft > 0) return true;
        else return false;
    }

    // Should never happen
    public override TileType doMoveFrom(Tile to, Move move)
    {
        return new RockTile();
    }

    // Should never happen

    public override TileType doMoveTo(Tile From, Move move)
    {
        gameStateManager.movesLeft--;
        PathTile newPath = new PathTile(Utils.oppositeMove(move), Move.None);
        newPath.init(gameStateManager);
        return newPath;
    }

    public override Sprite getSprite()
    {
        string spriteString = "breakableRockFrom"+Enum.GetName(typeof(Move), breakDirection);
        return gameStateManager.spriteLoader.getSprite(spriteString);
    }
}

public class PowerupTile : TileType
{

    private int value;
    GameStateManager gameStateManager;

    public PowerupTile(int value)
    {
        this.value=value;
    }

    public override void init(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    // Should never happen
    public override bool canMoveFrom(Move move)
    {
        return false;
    }

    public override bool canMoveTo(Tile from, Tile to, Move move)
    {
        if(gameStateManager.movesLeft == 0) return false;
        else return true;
    }

    // Should never happen
    public override TileType doMoveFrom(Tile to, Move move)
    {
        return new EmptyTile();
    }

    public override TileType doMoveTo(Tile From, Move move)
    {
        gameStateManager.movesLeft+=value;
        gameStateManager.movesLeft--;
        PathTile newPath = new PathTile(Utils.oppositeMove(move), Move.None);
        newPath.init(gameStateManager);
        return newPath;
    }

    public override Sprite getSprite()
    {
        return gameStateManager.spriteLoader.getSprite("powerup");;
    }

}

public class ExitTile : TileType
{

    GameStateManager gameStateManager;

    public override void init(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    // Should never happen
    public override bool canMoveFrom(Move move)
    {
        return false;
    }

    public override bool canMoveTo(Tile from, Tile to, Move move)
    {
        if(gameStateManager.movesLeft == 0) return false;
        else return true;
    }

    // Should never happen
    public override TileType doMoveFrom(Tile to, Move move)
    {
        return new EmptyTile();
    }

    // Should never happen

    public override TileType doMoveTo(Tile From, Move move)
    {
        gameStateManager.movesLeft--;
        PathTile newPath = new PathTile(Utils.oppositeMove(move), Move.None);
        newPath.init(gameStateManager);
        return newPath;
    }

    public override Sprite getSprite()
    {
        return gameStateManager.spriteLoader.getSprite("door");
    }

}