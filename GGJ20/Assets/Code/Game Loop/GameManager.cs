using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeLimit;
    int maxPieces;
    int piecesCollected;
    float time;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginGame()
    {
        piecesCollected = 0;
        maxPieces = (FindObjectsOfType<ObjectSlot>()).Length;
    }
}
