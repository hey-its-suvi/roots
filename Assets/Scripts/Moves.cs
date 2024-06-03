using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveDisplay : MonoBehaviour
{
    public TMP_Text movesLeftText;
    public GridManager gridManager;

    // Update is called once per frame
    void Update()
    {
        movesLeftText.text = gridManager.movesLeft.ToString();
    }
}
