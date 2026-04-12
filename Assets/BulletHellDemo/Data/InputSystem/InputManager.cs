using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public InputActions Actions;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Init()
    {
        if (Instance != null) return;

        GameObject gameObject = new GameObject("InputManager");
        Instance = gameObject.AddComponent<InputManager>();
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Actions = new InputActions();
        Actions.Enable();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Actions.Disable();
            Actions = null;
            Instance = null;
        }
    }
}
