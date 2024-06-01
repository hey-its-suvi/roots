using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isSelected;
    private SpriteRenderer selectedRenderer;

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

    public bool canMoveTo(Tile from, Move move)
    {
        return true;
    }
    public void selectTile()
    {
        isSelected = true;
        selectedRenderer.enabled = true;
    }

    public void deselectTile()
    {
        isSelected = false;
        selectedRenderer.enabled = false;

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
