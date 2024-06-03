using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int columns;
    public int movesLeft;

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
        Tile to = findToTile(from, move);

        // Check if theres any moves remaining
        if (movesLeft == 0 && !to.isTraversed){
            return false;
        }
        
        if(!from.canMoveFrom(move))
        {
            return false;
        }
        
        if(!to.canMoveTo(from, to, move))
        {
            return false;
        }
        return true;
        
    }

    void doMove(Move move)
    {
        Tile from = selectedTile;
        Tile to = findToTile(from, move);
        // If entering a new tile, update the prevTile
        // Dont want to change prevTile if we are backtracking
        if (!to.isTraversed){
            to.prevTile = from;
        }

        // Check if we are going forward or going backwards in our root
        bool isEnteringNewTile = false;
        if (!to.isTraversed){
            // We are entering a new tile
            isEnteringNewTile = true;
            movesLeft--;
        }
        else {
            movesLeft++;
        }
        from.deselectTile(isEnteringNewTile);
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

        // Initialize the number of moves left
        movesLeft = 100;
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
