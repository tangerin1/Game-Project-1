using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject donut;
    void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player") && Donut.donutCollected)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
