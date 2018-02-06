using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game", order = 1)]
public class GameSettings : ScriptableObject
{
    [Header("Speed")]
    [Tooltip("Min speed of balls")] public int MinSpeed = 100;
    [Tooltip("Max speed of balls")] public int MaxSpeed = 500;
    [Tooltip("Delta of change min speed with time")] public int SpeedDelta = 50;
    [Header("Size")]
    [Tooltip("Min size of balls")] public int MinSize = 100;
    [Tooltip("Max size of balls")] public int MaxSize = 300;
    [Header("Timers")]
    [Tooltip("Time of game in seconds")] public int GameTime = 60;
    [Tooltip("Each N seconds base speed of balls will be increased")] public int IncreaseSpeedTimeStep = 10;
    [Tooltip("Min Time of spawn balls in miliseconds")] public float SpawnDeltaMin = 0.1f;
    [Tooltip("Max Time of spawn balls in miliseconds")] public float SpawnDeltaMax = 1f;
    [Header("Points")]
    public int MinPoints = 1;
    public int MaxPoints = 5;
}
