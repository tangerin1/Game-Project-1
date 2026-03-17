using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TapeRecorder : MonoBehaviour, IInteractable
{
    [TextArea(4, 10)]
    public string[] logs;

    public TMP_Text logTextUI;
    public GameObject logPanel;

    private int currentLog = 0;
    private bool playerLooking = false;

    void Update()
    {
        if (!playerLooking || Mouse.current == null)
        {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (logPanel.activeSelf)
            {
                CloseLogAndAdvance();
            }
            else
            {
                OpenCurrentLog();
            }
        }
    }

    public void OnHoverEnter()
    {
        playerLooking = true;
    }

    public void OnHoverExit()
    {
        playerLooking = false;
    }

    void OpenCurrentLog()
    {
        if (logs == null || logs.Length == 0)
        {
            return;
        }

        logPanel.SetActive(true);
        logTextUI.text = logs[currentLog];
    }

    public void CloseLog()
    {
        logPanel.SetActive(false);
    }

    void CloseLogAndAdvance()
    {
        logPanel.SetActive(false);

        currentLog++;

        if (currentLog >= logs.Length)
        {
            currentLog = 0;
        }
    }
}