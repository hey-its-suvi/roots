using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isSelected;
    public bool isTraversed;
    private SpriteRenderer selectedRenderer;

    private SpriteRenderer spriteRenderer;

    public Tile prevTile;
    public TileType tileType;

    public int x;
    public int y;
    [SerializeField]private GameStateManager gameStateManager;

    public bool canMoveFrom(Move move)
    {
        if((x == 0 && move == Move.Down ) ||
           (x == gameStateManager.gridManager.rows-1 && move == Move.Up ) || 
           (y == 0 && move == Move.Left ) ||
           (y == gameStateManager.gridManager.columns-1 && move == Move.Right ))
        {
        return false;
        }
        return tileType.canMoveFrom(move);
    }

    public bool canMoveTo(Tile from, Tile to, Move move)
    {
        return to.tileType.canMoveTo(from, to, move);
        // Dont allow to move to tile if no moves are left and we are not backtracking
        // if(gameStateManager.movesLeft == 0 && !to.isTraversed)
        // {    
        //     return false;
        // }
        // // Do not allow backtrack if it is not the most recently traversed tile
        // if (to.isTraversed){
        //     if ( from.prevTile != to ){
        //         Debug.Log("to:(" + to.x + "," + to.y + "). prev:(" + prevTile.x + "," + prevTile.y + ")");
        //         Debug.Log("Not most recently traversed tile!");
        //         return false;
        //     }
        // }

        // // Cant move if rock
        // if (to.tileType == TileType.Rock){
        //     return false;
        // }
    }

    public void doMoveFrom(Tile to, Move move)
    {
        tileType = tileType.doMoveFrom(to,move);
        tileType.init(gameStateManager);
        spriteRenderer.sprite = tileType.getSprite();
    }

    public void doMoveTo(Tile from, Move move)
    {
        tileType = tileType.doMoveTo(from,move);
        tileType.init(gameStateManager);
        spriteRenderer.sprite = tileType.getSprite();
    }
    public void selectTile()
    {
        // isSelected = true;
        selectedRenderer.enabled = true;
        // selectedRenderer.color = new Color(.5f, .5f, .5f, .5f);
    }

    public void deselectTile(bool enteringNewTile)
    {
        // // isSelected = false;
        // if (enteringNewTile){
        //     selectedRenderer.color = Color.red;
        //     isTraversed = true;
        // }
        // else {
            selectedRenderer.enabled = false;
            // isTraversed = false;
        // }
    }

    public void init(int i, int j, int totalRows, int totalColumns, GameStateManager gameStateManager)
    {
        this.gameStateManager= gameStateManager;
        x=i;
        y=j;
        transform.position = new Vector3((float)(y-(totalColumns-1)/2f)/2*2f,(float)(x-(totalRows-1)/2f)/2*2f, 0);
        
        // Just for now, statically initialize start, finish, rocks, and powerups
        if (x == 5 && y == 1){
            tileType = new PathTile(Move.None, Move.None);
        }
        // if (x == 0 && y == 3){
        //     tileType = TileType.Finish;
        //     selectedRenderer.enabled = true;
        //     selectedRenderer.color = Color.green;
        // }
        else if ( (x == 5 && y == 0) || (x == 4 && y == 0) ||(x == 2 && y == 0) ||(x == 2 && y == 1)  ||(x == 3 && y == 2)){
            tileType = new RockTile();

        }
        else if (x == 3 && y == 0){
            tileType = new PowerupTile(5);
        }
        else if (x == 0 && y == 0){
            tileType = new ExitTile();
        }
        else if((x == 2 && y == 3))
        {
            tileType = new BreakableRockTile(Move.Right);
        }
        else
        {
            tileType = new EmptyTile();
        }
        tileType.init(gameStateManager);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tileType.getSprite();

    }

    void Awake()
    {
        Transform selected = transform.Find("Selected");
        if (selected != null)
        {
            selectedRenderer = selected.GetComponent<SpriteRenderer>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
