using Assets.ArchitectureLearn.Scripts.Game.GameRoot.Services;
using System;
using UnityEngine;

public class SomeGameplaySevice : IDisposable
{
    private readonly SomeCommonService _someCommonService;

    public SomeGameplaySevice(SomeCommonService someCommonService)
    {
        _someCommonService = someCommonService;
        Debug.Log(GetType().Name + " has been created");
    }

    public void Dispose()
    {
        Debug.Log("Подчистить все подписки");
    }
    
}
