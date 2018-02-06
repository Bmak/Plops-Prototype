using UnityEngine;

public class MenuService : BaseService
{
    private const string HIGHSCORE = "highscore";
    private int _currentHighScore = -1;

    public void SaveNewHighScore(int score)
    {
        if (GetHighScore() < score)
        {
            PlayerPrefs.SetInt(HIGHSCORE, score);
            _currentHighScore = score;
        }
    }

    public int GetHighScore()
    {
        if (_currentHighScore < 0)
        {
            _currentHighScore = LoadHighScore();
        }
        return _currentHighScore;
    }

    private int LoadHighScore()
    {
        if (PlayerPrefs.HasKey(HIGHSCORE))
        {
            return PlayerPrefs.GetInt(HIGHSCORE);
        }
        return 0;
    }

    public MenuService(GameSettings gameSettings) : base(gameSettings)
    {
    }
}
