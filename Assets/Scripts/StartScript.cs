using System;
using System.Collections.Generic;
using UnityEngine;

public interface IChangeState
{
    void InitSate<T>() where T : BaseController;
}

public class StartScript : MonoBehaviour, IChangeState
{
    private BaseController _currentState;

    private readonly Dictionary<Type, BaseController> _states = new Dictionary<Type, BaseController>();
    private Config _config;
    private ViewProvider _viewProvider;
    private ServiceProvider _serviceProvider;

    private void Start()
    {
        _config = new Config();
        _viewProvider = GetComponent<ViewProvider>();
        _viewProvider.Init(_config);
        _serviceProvider = GetComponent<ServiceProvider>();
        BindStates();

        //TODO do this after load all resources
        InitSate<MenuController>();
    }

    private void BindStates()
    {
        _states.Add(typeof(MenuController), CreateState<MenuController>());
        _states.Add(typeof(GameController), CreateState<GameController>());
    }

    private T CreateState<T>() where T : BaseController
    {
        BaseController c = Activator.CreateInstance<T>();
        c.InjectData(_viewProvider, _serviceProvider, this);
        return (T) c;
    }

    public void InitSate<T>() where T : BaseController
    {
        if (_currentState != null)
        {
            _currentState.Destroy();
        }

        BaseController state;
        _states.TryGetValue(typeof(T), out state);

        if (state != null)
        {
            _currentState = state;
            _currentState.Init();
            return;
        }

        throw new NullReferenceException("There's no such controller!!");
    }
}
