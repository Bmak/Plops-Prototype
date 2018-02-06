public class BaseController
{
    protected ViewProvider _viewProvider;
    protected ServiceProvider _serviceProvider;
    protected IChangeState _changeState;

    public void InjectData(ViewProvider viewProvider, ServiceProvider serviceProvider, IChangeState changeState)
    {
        _viewProvider = viewProvider;
        _serviceProvider = serviceProvider;
        _changeState = changeState;
    }

    public virtual void Init()
    {
    }

    public virtual void Destroy()
    {
    }
}
