using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelView : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _newHighScoreText;
    [SerializeField] private Button _exitButton;

    private Action _exitAction;

    public void Init(int currentScore, int maxScore, Action exitAction)
    {
        _scoreText.text = currentScore.ToString();
        _newHighScoreText.enabled = currentScore > maxScore;

        _exitAction = exitAction;
        _exitButton.onClick.AddListener(OnExitGame);
    }

    private void OnExitGame()
    {
        if (_exitAction != null)
        {
            _exitAction();
        }
    }

    private void OnDestroy()
    {
        _exitButton.onClick.RemoveAllListeners();
    }
}
