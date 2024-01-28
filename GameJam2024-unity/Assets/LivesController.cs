using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LivesController : MonoBehaviour
{
    public static LivesController Instance { get; private set; }
    private void Awake() { Instance = this; }
    public int player_Lives = 5;
}
