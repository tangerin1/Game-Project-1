using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class WinScreen : MonoBehaviour
{
    public GameObject donut;
    public TMP_Text text;
    
    void Start()
    {
        text.enabled = false;
        
    }
    void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player") && Donut.donutCollected)
        {
            SceneManager.LoadScene(1);
            Cursor.visible = true;
        }
        
        text.enabled = true;
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
