using System;
using System.Collections.Generic;

public class CompositeDisposable
{
    public List<IDisposable> Disposable { get; private set; }

    public void AddRange(IDisposable[] disposables)
    {
        Disposable.AddRange(disposables);
    }

    public void Add(IDisposable disposable)
    {
        Disposable.Add(disposable);
    }

    public void DisposeAll()
    {
        foreach(var item in Disposable)
        {
            item.Dispose();
        }
    }
}