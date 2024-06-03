using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isSelected;
    public bool isTraversed;
    private SpriteRenderer selectedRenderer;
    public Tile prevTile;

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
        return true;
    }

    public bool canMoveTo(Tile from, Tile to, Move move)
    {
        // TODO: Do not allow backtrack if it is not the most recently traversed tile
        
        if (to.isTraversed){
            if ( from.prevTile != to ){
                Debug.Log("to:(" + to.x + "," + to.y + "). prev:(" + prevTile.x + "," + prevTile.y + ")");
                Debug.Log("Not most recently traversed tile!");
                return false;
            }
        }
        return true;
    }
    public void selectTile()
    {
        // isSelected = true;
        selectedRenderer.enabled = true;
        selectedRenderer.color = new Color(.5f, .5f, .5f, .5f);
    }

    public void deselectTile(bool enteringNewTile)
    {
        // isSelected = false;
        if (enteringNewTile){
            selectedRenderer.color = Color.red;
            isTraversed = true;
        }
        else {
            selectedRenderer.enabled = false;
            isTraversed = false;
        }
    }

    public void init(int i, int j, int totalRows, int totalColumns, GameStateManager gameStateManager)
    {
        this.gameStateManager= gameStateManager;
        x=i;
        y=j;
        transform.position = new Vector3((float)(y-(totalColumns-1)/2f)/2*2f,(float)(x-(totalRows-1)/2f)/2*2f, 0);
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
