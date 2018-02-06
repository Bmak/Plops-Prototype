
using UnityEngine;

public class GameService : BaseService
{
    private int _speedStep = 0;

    public void Reset()
    {
        _speedStep = 0;
    }

    public int GameTime
    {
        get { return _settings.GameTime; }
    }

    public BallSettings GetBallSettings()
    {
        BallSettings ballSettings = new BallSettings();
        ballSettings.Size = Random.Range(_settings.MinSize, _settings.MaxSize);

        ballSettings.Speed = (int)UtilFunc.MapValue(ballSettings.Size, _settings.MinSize, _settings.MaxSize,
            _settings.MaxSpeed, _settings.MinSpeed) + _speedStep;

        ballSettings.StartPos = new Vector3(GetRandomXPos(ballSettings.Size), -GD.ScreenHeight/2-ballSettings.Size/2);
        ballSettings.Color = Random.ColorHSV();

        ballSettings.Points = (int) UtilFunc.MapValue(ballSettings.Speed,
            _settings.MinSpeed + _speedStep, _settings.MaxSpeed + _speedStep,
            _settings.MinPoints, _settings.MaxPoints + 1);

        return ballSettings;
    }

    private int GetRandomXPos(int size)
    {
        return Random.Range(-GD.ScreenWidth / 2 + size/2, GD.ScreenWidth / 2 - size/2);
    }

    public void IncreaseSpeed()
    {
        _speedStep += _settings.SpeedDelta;
    }

    public float GetIncreaseTime()
    {
        return _settings.IncreaseSpeedTimeStep;
    }

    public float GetSpawnTime()
    {
        return Random.Range(_settings.SpawnDeltaMin, _settings.SpawnDeltaMax);
    }

    public GameService(GameSettings gameSettings) : base(gameSettings)
    {
    }
}



