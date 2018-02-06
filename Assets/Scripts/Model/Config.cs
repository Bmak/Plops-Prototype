using System;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    private string _viewPrefabMenu = "ui.menu";
    private string _viewPrefabGame = "ui.game";
    private string _viewPrefabGameOver = "ui.game_over_panel";

    private readonly Dictionary<Type, string> _viewPrefabDict;

    public Config()
    {
        if (_viewPrefabDict == null)
        {
            _viewPrefabDict = new Dictionary<System.Type, string>
            {
                {typeof(MenuView), _viewPrefabMenu},
                {typeof(GameView), _viewPrefabGame},
                {typeof(GameOverPanelView), _viewPrefabGameOver}
            };
        }
    }

    public string GetViewPrefabName(Type objType)
    {
        string prefabName = string.Empty;
        _viewPrefabDict.TryGetValue(objType, out prefabName);
        return prefabName;
    }
}
