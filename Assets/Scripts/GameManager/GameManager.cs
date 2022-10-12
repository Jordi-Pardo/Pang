using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum Dificulty
    {
        Easy,
        Normal,
        Hard,
    }

    private const int POWER_UP_SPAWN_RATIO = 10;
    public List<GameObject> powerUps = new List<GameObject>();

    //Singleton
    public static GameManager instance;

    //Events
    public event Action<string,string> OnPlayerWin;
    public event Action OnPlayerLose;
    public event Action<string> OnUpdateTime;
    public event Action<string> OnUpdateScore;

    //Dificulty
    [Header("Dificulty")]
    [SerializeField] private Dificulty dificulty;

    //Balls
    [Header("Balls")]
    [SerializeField] private Transform ballsContainer;
    [SerializeField] private Transform powerUpsContainer;
    private List<Ball> ballsList;


    //Time
    private float gameTime;
    private float initTime = 20f;

    //Score
    private int _score = 0;

    public int Score { get => _score; set => _score = value; }

    public bool Freezed = false;

    private void Awake()
    {
        instance = this;

        Player.OnDead += PlayerLose;
        Ball.OnBallDestroyed += UpdateScore;

    }

    private void Start()
    {
        AddAllBalls();
        DifficultySettings();
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

    private void DifficultySettings()
    {
        switch (dificulty)
        {
            case Dificulty.Easy:
                initTime -= 5f;
                break;
            case Dificulty.Normal:
                initTime -= 10f;
                break;
            case Dificulty.Hard:
                initTime -= 15f;
                break;
        }
    }

    public void AddBall(Ball ball)
    {
        if (Freezed)
            ball.startedFreezed = true;

        ballsList.Add(ball);
    }

    private void AddAllBalls()
    {
        ballsList = new List<Ball>();

        foreach (Transform ball in ballsContainer)
        {   
            
            AddBall(ball.GetComponent<Ball>());
        }

        initTime += ballsList.Count * 10f;
    }
    private void UpdateBallsList(Ball ball)
    {
        ballsList.Remove(ball);
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

    private void UpdateScore(int score, Ball ball)
    {
        Score += score;
        UpdateBallsList(ball);
        OnUpdateScore?.Invoke(Score.ToString());
        TryToSpawnPowerUp(ball.transform.position);
    }

    private void TryToSpawnPowerUp(Vector3 position)
    {
        int randomProbability = UnityEngine.Random.Range(0, 100);
        if(randomProbability < POWER_UP_SPAWN_RATIO)
        {
            //Can Spawn
            int randomIndex = UnityEngine.Random.Range(0, powerUps.Count);
            GameObject powerUp = Instantiate(powerUps[randomIndex], position, Quaternion.identity,powerUpsContainer);
            if(powerUp.TryGetComponent(out IPowerUp iPowerUp)){
                iPowerUp.OnSpawn();
            }
        }
    }
    public void StartStopWatch()
    {
        StartCoroutine(StartFreeze());
    }
    public IEnumerator StartFreeze()
    {
        StopAllBalls();
        Debug.Log("Stopped all balls");
        yield return new WaitForSeconds(5f);
        Debug.Log("Resume all balls");
        ResumeAllBalls();
    }

    public void StopAllBalls()
    {
        foreach (var ball in ballsList)
        {
            ball.GetComponent<Ball>().Freeze();
        }
        Freezed = true;
    }

    public void ResumeAllBalls()
    {
        foreach (var ball in ballsList)
        {
            if (ball.startedFreezed)
            {
                ball.ResumeFromFreezed();
            }
            else
            {
                ball.Resume();
            }

        }
        Freezed = false;
    }
   

    private void OnDestroy()
    {
        Player.OnDead -= PlayerLose;
        Ball.OnBallDestroyed -= UpdateScore;
    }
}
