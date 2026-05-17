using UnityEngine;

public class UnityBridgeManager : MonoBehaviour
{
   public static UnityBridgeManager Instance { get; private set; }

    // [Header("Message Routing")]
    // [SerializeField] private AnimationJsonSerializer animationJsonSerializer;
    // [SerializeField] private ItemJsonSerializer itemJsonSerializer;
    // [SerializeField] private UIManager uiManager;
    

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void UJSB_NotifyReady();
    [DllImport("__Internal")] private static extern void UJSB_SendToJS(string message);
    [DllImport("__Internal")] private static extern void UJSB_SendToFlutter(string message);
#endif

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameObject.name = "UnityJSBridge";
        Debug.Log("[UnityJSBridge] Awake — GameObject name: " + gameObject.name);

    //     if (animationJsonSerializer == null)
    //         animationJsonSerializer = FindObjectOfType<AnimationJsonSerializer>();

    //     if (uiManager == null)
    //         uiManager = FindObjectOfType<UIManager>();

    //     if (itemJsonSerializer == null)
    //         itemJsonSerializer = FindObjectOfType<ItemJsonSerializer>();
     }

    private void Start()
    {
        Debug.Log("[UnityJSBridge] Start — signalling JS that C# is ready");
#if UNITY_WEBGL && !UNITY_EDITOR
        UJSB_NotifyReady();
#endif
    }

    public void OnMessageFromJS(string json)
    {
        Debug.Log("[UnityJSBridge] JS → C#: " + json);

        // if (uiManager != null)
        //     uiManager.GetData(json);

        // if (animationJsonSerializer != null)
        //     animationJsonSerializer.ProcessJson(json);
        // else
        //     Debug.LogWarning("[UnityJSBridge] No AnimationJsonSerializer assigned.");

        // if (itemJsonSerializer != null)
        //     itemJsonSerializer.OnItemsMessageFromFlutter(json);
        // else
        //     Debug.LogWarning("[UnityJSBridge] No ItemJsonSerializer assigned.");
    }

    public void OnMessageFromFlutter(string json)
    {
        Debug.Log("[UnityJSBridge] Flutter → C#: " + json);

        // if (uiManager != null)
        //     uiManager.GetData(json);

        // if (animationJsonSerializer != null)
        //     animationJsonSerializer.ProcessJson(json);
        // else
        //     Debug.LogWarning("[UnityJSBridge] No AnimationJsonSerializer assigned.");

        // if (itemJsonSerializer != null)
        //     itemJsonSerializer.OnItemsMessageFromFlutter(json);
        // else
        //     Debug.LogWarning("[UnityJSBridge] No ItemJsonSerializer assigned.");
    }

    public void SendToJS(string message)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        UJSB_SendToJS(message);
#else
        Debug.Log("[UnityJSBridge][Editor] SendToJS: " + message);
#endif
    }

    public void SendToFlutter(string message)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        UJSB_SendToFlutter(message);
#else
        Debug.Log("Unity To Flutter: " + message);
#endif
    }

    public static void Emit(string message) => Instance?.SendToJS(message);
    public static void EmitToFlutter(string message) => Instance?.SendToFlutter(message);
}
