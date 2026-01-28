using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(1);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
