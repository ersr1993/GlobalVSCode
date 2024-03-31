using System;

namespace ConsViSa.DI;
public class AbstractFactory<T> : IAbstractFactory<T>
{
    private readonly Func<T> _factory;
    public AbstractFactory(Func<T> factory)
    {
        _factory = factory;
    }
    public T Instanciate()
    {
        T instanciatedObject;
        instanciatedObject = _factory.Invoke();
        return instanciatedObject;
    }
}
