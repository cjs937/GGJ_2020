using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text timer;
    public Text pointsText;

    public GameObject gameOverScreen;
    public Text winText;
    public Text loseText;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        gameOverScreen.SetActive(false);
        winText.enabled = false;
        loseText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = gameManager.time.ToString();
        pointsText.text = gameManager.piecesCollected.ToString() + " / " + gameManager.maxPieces.ToString();
    }

    //Brings up the game over screen and shows the right message depending on the win state
    public void GameOver(bool win)
    {
        gameOverScreen.SetActive(true);

        if (win)
            winText.enabled = true;
        else
            loseText.enabled = true;
    }
}
