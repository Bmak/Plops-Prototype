using UnityEngine;

public class MenuController : BaseController
{
    private MenuView _menuView;

    public override void Init()
    {
        _viewProvider.Get<MenuView>(view =>
        {
            _menuView = view;

            MenuRenderData renderData = new MenuRenderData();
            renderData.StartGame = OnStartGame;
            renderData.HighScore = _serviceProvider.Get<MenuService>().GetHighScore();

            _menuView.Init(renderData);
            _menuView.gameObject.SetActive(true);
        });
    }

    private void OnStartGame()
    {
        _changeState.InitSate<GameController>();
    }

    public override void Destroy()
    {
        GameObject.Destroy(_menuView.gameObject);
    }

}
