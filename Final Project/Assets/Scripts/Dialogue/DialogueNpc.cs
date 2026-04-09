using UnityEngine;
using UnityEngine.InputSystem;

/*
this script goes on an npc 
it waits for the player to walk up and press e then starts that npc's ink dialogue
*/
public class DialogueNpc : MonoBehaviour
{
    // compiled ink json file for this npc
    public TextAsset inkJson;

    // dialogue manager in the scene that shows the ui
    public InkDialogueManager dialogueManager;

    // optional prompt for interaction ("press e to talk")
    public GameObject interactPrompt;

    // tracks if the player is standing in range
    private bool _playerInRange;

    // ensure the prompt starts hidden
    private void Start()
    {
        if (dialogueManager == null)
        {
            dialogueManager = FindFirstObjectByType<InkDialogueManager>();
        }

        // hide the prompt until the player walks up
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    // listens for the interact key while player is in range of npc
    private void Update()
    {
        if (!_playerInRange)
        {
            return;
        }

        if (dialogueManager == null)
        {
            return;
        }

        // hide prompt while any dialogue is open
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(!dialogueManager.IsDialogueOpen);
        }

        // do not let npc restart dialogue while one is already open
        if (dialogueManager.IsDialogueOpen)
        {
            return;
        }

        // start this npc's conversation when interact key is pressed
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            dialogueManager.StartDialogue(inkJson);
        }
    }

    // checks if player has entered the trigger around the npc
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SimpleCharacterController>() == null)
        {
            return;
        }

        _playerInRange = true;

        // show the prompt if dialogue is not already open
        if (interactPrompt != null && dialogueManager != null && !dialogueManager.IsDialogueOpen)
        {
            interactPrompt.SetActive(true);
        }
    }

    // checks if player has left the trigger around the npc
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SimpleCharacterController>() == null)
        {
            return;
        }

        _playerInRange = false;

        // hide the talk prompt when the player walks away
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }
}
