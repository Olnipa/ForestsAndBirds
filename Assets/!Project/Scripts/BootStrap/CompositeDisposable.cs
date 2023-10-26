using System;
using System.Collections.Generic;

public class CompositeDisposable
{
    private List<IDisposable> Disposable = new List<IDisposable>();

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