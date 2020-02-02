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

    public Camera eyeCam;
    public Camera followCam;
    Camera mainCamera;

    private void Start()
    {
        UI = FindObjectOfType<GameUI>();
        player = GetComponent<CharController>();

        mainCamera = eyeCam;
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

        if (Input.GetButtonDown("CameraSwitch"))
            SwapCameras();
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

    public void SwapCameras()
    {
        Camera otherCamera;

        if (mainCamera == eyeCam)
        {
            mainCamera = followCam;
            otherCamera = eyeCam;
        }
        else
        {
            mainCamera = eyeCam;
            otherCamera = followCam;
        }


        otherCamera.tag = "Untagged";
        otherCamera.targetTexture = mainCamera.targetTexture;

        mainCamera.tag = "MainCamera";
        mainCamera.targetTexture = null;
        
    }
}
