
using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceProvider : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    private Dictionary<Type, object> _serviceDict;

    private void Awake()
    {
        if (_serviceDict == null)
        {
            _serviceDict = new Dictionary<System.Type, object>
            {
                {typeof(GameService), new GameService(_gameSettings)},
                {typeof(MenuService), new MenuService(_gameSettings)}
            };
        }
    }

    public TService Get<TService>() where TService : class
    {
        object service = null;
        _serviceDict.TryGetValue(typeof(TService), out service);
        if (service != null)
        {
            return service as TService;
        }
        throw new NullReferenceException("There's no such service!!");
    }
}
