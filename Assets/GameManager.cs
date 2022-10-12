using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;

    //Events
    public event Action<string,string> OnPlayerWin;
    public event Action OnPlayerLose;
    public event Action<string> OnUpdateTime;
    public event Action<string> OnUpdateScore;

    //Balls
    [Header("Balls")]
    [SerializeField] private Transform ballsParent;
    private List<GameObject> ballsList;


    //Time
    private float gameTime;
    private float initTime = 20f;

    //Score
    private int _score = 0;

    public int Score { get => _score; set => _score = value; }

    private void Awake()
    {
        instance = this;

        PlayerMovement.OnDead += PlayerLose;
        BallMovement.OnUpdateScore += UpdateScore;

    }

    private void Start()
    {
        AddAllBalls();
        SetupTimer();

        Time.timeScale = 1;

    }

    private void Update()
    {
       UpdateTimer();
    }

    private void SetupTimer()
    {
        gameTime = initTime;
        OnUpdateTime?.Invoke(Mathf.CeilToInt(gameTime).ToString());
    }

    public void AddBall(GameObject ball)
    {
        ballsList.Add(ball);
    }

    private void AddAllBalls()
    {
        ballsList = new List<GameObject>();

        foreach (Transform ball in ballsParent)
        {
            AddBall(ball.gameObject);
        }

        initTime += ballsList.Count * 10f;
    }
    private void UpdateBallsList(GameObject gameObject)
    {
        ballsList.Remove(gameObject);
        if(ballsList.Count == 0)
        {
            PlayerWins();
        }
    }


    public void UpdateTimer()
    {
        if (gameTime <= 0)
            return;

        int firstTimeInt = Mathf.CeilToInt(gameTime);
        gameTime -= Time.deltaTime;

        int gameTimeInt = Mathf.CeilToInt(gameTime);


        if((firstTimeInt - gameTimeInt) == 1)
        {
            OnUpdateTime?.Invoke(gameTimeInt.ToString());

            if (gameTimeInt == 0)
            {
               PlayerLose();
            }
        }

    }

    private void PlayerWins()
    {
        OnPlayerWin?.Invoke($"Score: {Score}",$"Time: {Mathf.CeilToInt(gameTime)}");
        Time.timeScale = 0;

    }

    private void PlayerLose()
    {
        OnPlayerLose?.Invoke();
        Time.timeScale = 0;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnDead -= PlayerLose;
        BallMovement.OnUpdateScore -= UpdateScore;
    }

    private void UpdateScore(int score, GameObject gameObject)
    {
        Score += score;
        UpdateBallsList(gameObject);
        OnUpdateScore?.Invoke(Score.ToString());
    }
}
