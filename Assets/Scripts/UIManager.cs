using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    [SerializeField] GameStateManager gameStateManager;
    private void onTriggerWin()
    {
        if(gameStateManager.gridManager.hasWon())
        {
            winUI.SetActive(true);
            GameStateManager.isPaused = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        winUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        onTriggerWin();
    }
}
