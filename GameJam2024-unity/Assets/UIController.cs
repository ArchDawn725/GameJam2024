using UnityEngine;
public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    private void Awake() { Instance = this; }

    [SerializeField] GameObject[] healthIcons;
    [SerializeField] GameObject lifeIcon;
    [SerializeField] Transform lifeTransform;

    public void UpdateHealth(int currentHealth)
    {
        if (currentHealth >= 3) { healthIcons[2].SetActive(true); healthIcons[1].SetActive(true); healthIcons[0].SetActive(true); }
        if (currentHealth == 2) { healthIcons[2].SetActive(false); healthIcons[1].SetActive(true); healthIcons[0].SetActive(true); }
        if (currentHealth == 1) { healthIcons[2].SetActive(false); healthIcons[1].SetActive(false); healthIcons[0].SetActive(true); }
        if (currentHealth == 0) { healthIcons[2].SetActive(false); healthIcons[1].SetActive(false); healthIcons[0].SetActive(false); }
    }
    public void UpdateLives(int currentLives)
    {
        for(int i = 0; i < LivesController.Instance.player_Lives; i++)
        {
            GainLife();
        }
    }
    public void GainLife()
    {
        Instantiate(lifeIcon, lifeTransform);
    }
}
