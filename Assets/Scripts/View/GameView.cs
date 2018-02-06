using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRenderData
{
    public GameService GameService { get; set; }
    public Action<int> GameFinished { get; set; }
}

public class GameView : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timerText;
    [SerializeField] private BallView _ballPrefab;

    private GameRenderData _renderData;

    private int _currentPoints = 0;

    private CountDownTimer _gameTimer;
    private long _gameTimeLeft;
    private float _spawnTimeLeft;
    private float _increaseSpeedTimeLeft;

    private List<BallView> _activeBalls = new List<BallView>();
    private const int POOL_START_SIZE = 15;
    private List<BallView> _pool = new List<BallView>();

    public void Init(GameRenderData renderData)
    {
        _renderData = renderData;
        InitPool();
        StartGame();
    }

    private void StartGame()
    {
        _increaseSpeedTimeLeft = _renderData.GameService.GetIncreaseTime();
        _currentPoints = 0;
        _scoreText.text = string.Format("Score: {0}", _currentPoints);
        GD.GAME_PAUSED = false;
        StartGameTimer();
        StartSpawnTimer();
        
    }

    private void StartSpawnTimer()
    {
        _spawnTimeLeft = _renderData.GameService.GetSpawnTime();
    }

    private void Update()
    {
        if (GD.GAME_PAUSED) return;

        _spawnTimeLeft -= Time.deltaTime;
        if (_spawnTimeLeft <= 0)
        {
            SpawnBall();
            StartSpawnTimer();
        }

        _increaseSpeedTimeLeft -= Time.deltaTime;
        if (_increaseSpeedTimeLeft <= 0)
        {
            _renderData.GameService.IncreaseSpeed();
            _increaseSpeedTimeLeft = _renderData.GameService.GetIncreaseTime();
        }
    }

    public void StartGameTimer()
    {
        if (_gameTimer == null)
        {
            _gameTimer = gameObject.AddComponent<CountDownTimer>();
        }
        _gameTimer.StartTimer(_renderData.GameService.GameTime, UpdateGameTimer, FinishGameTimer);
    }

    private void UpdateGameTimer(long timeLeft)
    {
        _gameTimeLeft = timeLeft;
        _timerText.text = UtilFunc.FormatSecondsToMinutes((int) _gameTimeLeft);
    }

    private void FinishGameTimer()
    {
        _timerText.text = "00:00";

        while (_activeBalls.Count > 0)
        {
            ReturnToPool(_activeBalls[0]);
        }

        GD.GAME_PAUSED = true;

        if (_renderData.GameFinished != null)
        {
            _renderData.GameFinished(_currentPoints);
        }
    }

    private void InitPool()
    {
        for (int i = 0; i < POOL_START_SIZE; i++)
        {
            _pool.Add(CreateBall());
        }
    }

    private void SetBallSettings(BallView ball)
    {
        ball.Settings = _renderData.GameService.GetBallSettings();
    }

    private BallView CreateBall()
    {
        BallView ball = Instantiate(_ballPrefab, gameObject.transform);
        SetBallSettings(ball);
        ball.gameObject.SetActive(false);
        ball.ReturnToPool = ReturnToPool;
        ball.AddPoints = AddPoints;
        ball.Init();
        return ball;
    }

    private void AddPoints(int points)
    {
        _currentPoints += points;
        _scoreText.text = string.Format("Score: {0}", _currentPoints);
    }

    private void ReturnToPool(BallView ball)
    {
        _activeBalls.Remove(ball);
        _pool.Add(ball);
        ball.gameObject.SetActive(false);
        SetBallSettings(ball);
        ball.Init();
    }

    private void SpawnBall()
    {
        if (_pool.Count == 0)
        {
            _pool.Add(CreateBall());
        }
        BallView ball = _pool[0];
        _pool.RemoveAt(0);
        _activeBalls.Add(ball);
        ball.gameObject.SetActive(true);
    }
}
