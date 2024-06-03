using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveDisplay : MonoBehaviour
{
    public TMP_Text movesLeftText;
    public GameStateManager gameStateManager;

    // Update is called once per frame
    void Update()
    {
        movesLeftText.text = "Moves Left:" + gameStateManager.movesLeft.ToString();
    }
}
