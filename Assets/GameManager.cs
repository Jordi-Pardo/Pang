using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;

    public Action OnPlayerDie;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        OnPlayerDie += ShowMenu;
        Time.timeScale = 1;
    }

    public void ShowMenu()
    {
        menuCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
