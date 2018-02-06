using UnityEngine;

public class GameController : BaseController
{
    private GameView _gameView;
    private GameOverPanelView _gameOverPanel;

    private GameService _gameService;
    private MenuService _menuService;

    private int _currentScore;

    public override void Init()
    {
        _gameService = _serviceProvider.Get<GameService>();
        _menuService = _serviceProvider.Get<MenuService>();
        _gameService.Reset();
        _viewProvider.Get<GameView>(view =>
        {
            _gameView = view;

            GameRenderData renderData = new GameRenderData();
            renderData.GameService = _gameService;
            renderData.GameFinished = OnGameFinished;

            _gameView.gameObject.SetActive(true);
            _gameView.Init(renderData);
        });
    }

    private void OnGameFinished(int score)
    {
        _currentScore = score;

        if (_gameOverPanel != null)
        {
            GameObject.Destroy(_gameOverPanel.gameObject);
            _gameOverPanel = null;
        }

        _viewProvider.Get<GameOverPanelView>(view =>
        {
            _gameOverPanel = view;
            _gameOverPanel.Init(_currentScore, _menuService.GetHighScore(), OnExitGame);
            _gameOverPanel.gameObject.SetActive(true);
        });
    }

    private void OnExitGame()
    {
        if (_currentScore > _menuService.GetHighScore())
        {
            _menuService.SaveNewHighScore(_currentScore);
        }

        GameObject.Destroy(_gameOverPanel.gameObject);

        _changeState.InitSate<MenuController>();
    }

    public override void Destroy()
    {
        GameObject.Destroy(_gameView.gameObject);
    }
}
