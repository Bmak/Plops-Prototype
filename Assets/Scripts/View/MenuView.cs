using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuRenderData
{
    public Action StartGame { get; set; }
    public int HighScore { get; set; }
}

public class MenuView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Text _highScoresText;

    private MenuRenderData _renderData;

    public void Init(MenuRenderData renderData)
    {
        _renderData = renderData;
        _startButton.onClick.AddListener(OnStartGame);
        _highScoresText.text = string.Format("HighScore: {0}", renderData.HighScore);
    }

    private void OnStartGame()
    {
        if (_renderData.StartGame != null)
        {
            _renderData.StartGame();
        }
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
    }
}
