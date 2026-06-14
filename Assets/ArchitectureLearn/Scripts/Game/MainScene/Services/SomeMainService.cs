using Assets.ArchitectureLearn.Scripts.Game.GameRoot.Services;
using UnityEngine;

public class SomeMainService
{
    private readonly SomeCommonService _someCommonService;

    public SomeMainService(SomeCommonService someCommonService)
    {
        _someCommonService = someCommonService;
        Debug.Log(GetType().Name + " has been created");
    }
}
