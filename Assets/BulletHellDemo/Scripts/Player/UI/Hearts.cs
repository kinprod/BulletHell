using UnityEngine;
using System.Collections.Generic;

public class Hearts : MonoBehaviour
{
    [SerializeField]
    private List<Heart> _hearts;

    private void Awake()
    {
        if (_hearts.Count > 0)
        {
            _hearts[_hearts.Count - 1].EnableHeart();
            _hearts[_hearts.Count - 1].DestroyHeartAction += DestroyHeart;
        }
    }

    private void DestroyHeart()
    {
        if (_hearts.Count > 0)
        {
            Heart heart = _hearts[_hearts.Count - 1];
            _hearts.Remove(heart);
            Destroy(heart.gameObject);
            if (_hearts.Count != 0)
            {
                _hearts[_hearts.Count - 1].EnableHeart();
                _hearts[_hearts.Count - 1].DestroyHeartAction += DestroyHeart;
            }
        }
    }
}
