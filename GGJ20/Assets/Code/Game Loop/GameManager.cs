using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool inGame = false;
    public float timeLimit;
    GameUI UI;
    CharController player;
    
    public int maxPieces;
    public int piecesCollected;
    public float time;

    private void Start()
    {
        UI = FindObjectOfType<GameUI>();
        player = GetComponent<CharController>();
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(inGame)
        {
            time -= Time.deltaTime;
            time = Mathf.Max(0, time);
            
            if(time == 0)
            {
                EndGame(false);
            }
        }
    }

    public void BeginGame()
    {
        piecesCollected = 0;
        maxPieces = (FindObjectsOfType<ObjectSlot>()).Length;
        time = timeLimit;

        inGame = true;
    }

    public void AddToScore()
    {
        ++piecesCollected;

        if (piecesCollected >= maxPieces)
            EndGame(true);
    }

    public void EndGame(bool win)
    {
        player.enabled = false;

        UI.GameOver(win);
        inGame = false;
        //Show game over screen

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
