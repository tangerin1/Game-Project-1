using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DivorcePaperThought : MonoBehaviour, IInteractable
{
    [TextArea(4, 10)]
    public string thoughtText;

    public TMP_Text thoughtTextUI;
    public GameObject thoughtPanel;

    private bool playerLooking = false;

    void Update()
    {
        if (!playerLooking || Mouse.current == null)
        {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (thoughtPanel.activeSelf)
            {
                CloseThought();
            }
            else
            {
                OpenThought();
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

    void OpenThought()
    {
        thoughtPanel.SetActive(true);
        thoughtTextUI.text = thoughtText;
    }

    public void CloseThought()
    {
        thoughtPanel.SetActive(false);
    }
}