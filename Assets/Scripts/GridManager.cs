using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int columns;
    [SerializeField] private GameObject tilePrefab;

    [SerializeField] int startTilex;

    [SerializeField] int startTiley;


    private Tile[,] tiles;
    private Tile selectedTile;

    [SerializeField]private GameStateManager gameStateManager;


    void HandleInput()
    {
        Move move = Move.None;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            move = Move.Up; 
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            move = Move.Down; 
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move = Move.Left;    
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move = Move.Right; 
        }
        if(move !=Move.None)
        {
            if(canDoMove(move))
            {
                doMove(move);
            }
        }

    }

    bool canDoMove(Move move)
    {
        Tile from = selectedTile;
        if(!from.canMoveFrom(move))
        {
            return false;
        }
        Tile to = findToTile(from, move);
        if(!to.canMoveTo(from,move))
        {
            return false;
        }
        return true;
        
    }

    void doMove(Move move)
    {
        Tile from = selectedTile;
        Tile to = findToTile(from, move);
        from.deselectTile();
        to.selectTile();
        selectedTile = to;
    }

    private Tile findToTile(Tile from, Move move)
    {
        if(move==Move.Up)
        {
            return tiles[from.x+1, from.y];
        }
        if(move==Move.Down)
        {
            return tiles[from.x-1, from.y];
        }
        if(move==Move.Left)
        {
            return tiles[from.x, from.y-1];
        }
        if(move==Move.Right)
        {
            return tiles[from.x, from.y+1];
        }
        return from;
    }

    void SetupTileGrid()
    {
        tiles = new Tile[rows, columns];
        for(int i=0;i<rows; i++)
        {
            for(int j=0;j<columns;j++)
            {
                GameObject tileObject = Instantiate(tilePrefab, transform.position, Quaternion.identity);
                Tile tile = tileObject.GetComponent<Tile>();
                tile.init(i,j,rows, columns, gameStateManager);
                tiles[i,j] = tile;
            }
        }
        
        selectedTile=tiles[startTilex,startTiley];
        selectedTile.selectTile();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupTileGrid();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

}
