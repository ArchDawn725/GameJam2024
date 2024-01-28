using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LivesController : MonoBehaviour
{
    public static LivesController Instance { get; private set; }
    private void Awake() { Instance = this; }
    public int player_Lives = 5;
    public Vector2 lastPlayerDeath;
    [SerializeField] private GameObject oil;
    public void UpdateUILives() { UIController.Instance.UpdateLives(player_Lives); }
    public void Loaded_Level() { if (lastPlayerDeath != new Vector2(0,0)) { Instantiate(oil, lastPlayerDeath, Quaternion.identity); lastPlayerDeath = new Vector2(0,0); } }
}
