﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

    public void GameOver ()
    {
        _isGameOver = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0); //current game scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
