using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] GameStateManager gameStateManager;
    [SerializeField] GridManager gridManager;

    private void onTriggerWin()
    {
        Tile selectedTile = gridManager.getSelectedTile();
        // Trigger Win
        if (selectedTile.x == gridManager.winTilex && selectedTile.y == gridManager.winTiley) {
            _winCanvas.GetComponent<Canvas>().enabled = true;
            GameStateManager.isPaused = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _winCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        onTriggerWin();
    }
}
