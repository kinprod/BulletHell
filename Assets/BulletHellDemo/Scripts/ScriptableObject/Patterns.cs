using UnityEngine;

[CreateAssetMenu(fileName = "Patterns", menuName = "Scriptable Objects/Patterns")]
public class Patterns : ScriptableObject
{
    [SerializeField]
    private AnimationCurve[] _curvedPatterns;

    public AnimationCurve[] GetCurvedPatterns()
    {
        return _curvedPatterns;
    }
}
