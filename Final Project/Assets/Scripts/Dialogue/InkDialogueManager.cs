using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*
runs the basic ink dialogue ui
it opens a story, shows the current text, and lets the player click choices
*/
public class InkDialogueManager : MonoBehaviour
{
    // the dialogue panel 
    public GameObject dialoguePanel;

    // text area that shows the current dialogue
    public TMP_Text dialogueText;

    // the choice buttons 
    public Button[] choiceButtons;

    // text labels for the choice buttons
    public TMP_Text[] choiceButtonTexts;

    // player controller script so we can freeze movement during dialogue
    public SimpleCharacterController playerController;

    // mouse look script so we can stop camera movement during dialogue
    public MouseLookInputSystem mouseLook;

    // the active ink story while a conversation is open
    private Story _currentStory;

    // the apprentice light handler for the next dialogue 
    private ApprenticeLightDialogue _pendingApprenticeLightDialogue;

    // is dialogue open right now
    private bool _isDialogueOpen;

    // lets other scripts check if dialogue is already running
    public bool IsDialogueOpen => _isDialogueOpen;

    // sets up the button click events 
    private void Awake()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<SimpleCharacterController>();
        }

        if (mouseLook == null)
        {
            mouseLook = FindFirstObjectByType<MouseLookInputSystem>();
        }

        // ensure ui starts hidden
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        // connect each button to its matching choice slot
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (choiceButtons[i] == null)
            {
                continue;
            }

            int buttonIndex = i;
            choiceButtons[i].onClick.AddListener(() => ChooseChoice(buttonIndex));
        }
    }

    // let the player close a conversation with escape
    private void Update()
    {
        if (!_isDialogueOpen)
        {
            return;
        }

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EndDialogue();
        }
    }

    // starts a new dialogue from a compiled ink json file
    public void StartDialogue(TextAsset inkJson)
    {
        if (inkJson == null)
        {
            Debug.LogWarning("missing ink json file on dialogue start");
            return;
        }

        // build a fresh story every time you talk to npc
        _currentStory = new Story(inkJson.text);
        BindExternalDialogueFunctions();
        _currentStory.onError += HandleStoryError;
        _isDialogueOpen = true;

        // show the ui
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        // freeze player movement
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // freeze camera control
        if (mouseLook != null)
        {
            mouseLook.enabled = false;
        }

        AdvanceDialogue();
    }
    
    public void SetApprenticeLightDialogue(ApprenticeLightDialogue apprenticeLightDialogue)
    {
        _pendingApprenticeLightDialogue = apprenticeLightDialogue;
    }

    // closes the current dialogue and unfreezes movement
    public void EndDialogue()
    {
        // clear active story data
        if (_currentStory != null)
        {
            _currentStory.onError -= HandleStoryError;
        }

        _currentStory = null;
        _isDialogueOpen = false;

        // hide dialogue ui
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        // clear any leftover text
        if (dialogueText != null)
        {
            dialogueText.text = "";
        }

        // hide choice buttons
        HideAllChoices();

        // unfreeze movement
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // unfreeze camera
        if (mouseLook != null)
        {
            mouseLook.enabled = true;
        }
    }

    // moves the story forward by one visible line
    private void AdvanceDialogue()
    {
        if (_currentStory == null)
        {
            return;
        }

        // if there is nothing left to continue just redraw the buttons
        if (!_currentStory.canContinue)
        {
            ShowButtons();
            return;
        }

        string nextLine = "";

        // keep pulling until we hit a real line
        while (_currentStory.canContinue && string.IsNullOrEmpty(nextLine))
        {
            try
            {
                nextLine = _currentStory.Continue().Trim();
            }
            catch (StoryException)
            {
                // stop trying to continue if ink ran out unexpectedly
                break;
            }
        }

        // show only one line at a time
        if (dialogueText != null && !string.IsNullOrEmpty(nextLine))
        {
            dialogueText.text = nextLine;
        }

        // redraw buttons for the current moment
        ShowButtons();
    }

    // decides whether to show continue or choices
    private void ShowButtons()
    {
        HideAllChoices();

        if (_currentStory == null)
        {
            return;
        }

        // if ink is waiting on a choice show the branch buttons
        if (_currentStory.currentChoices.Count > 0)
        {
            ShowChoiceButtons();
            SetupCloseButton();
            return;
        }

        // if more lines are coming show continue
        if (_currentStory.canContinue)
        {
            SetupContinueButton();
        }

        // keep the end button visible the whole time
        SetupCloseButton();
    }

    // fills the visible branch choice buttons
    private void ShowChoiceButtons()
    {
        int visibleChoiceCount = Mathf.Min(_currentStory.currentChoices.Count, GetChoiceButtonLimit());

        // warning if ink file tries to show too many choices
        if (_currentStory.currentChoices.Count > GetChoiceButtonLimit())
        {
            Debug.LogWarning("too many dialogue choices only the buttons in the scene will show");
        }

        // show one button for each real dialogue choice
        for (int i = 0; i < visibleChoiceCount && i < choiceButtons.Length; i++)
        {
            if (choiceButtons[i] == null)
            {
                continue;
            }

            choiceButtons[i].gameObject.SetActive(true);

            if (i < choiceButtonTexts.Length && choiceButtonTexts[i] != null)
            {
                choiceButtonTexts[i].text = _currentStory.currentChoices[i].text.Trim();
            }

            choiceButtons[i].onClick.RemoveAllListeners();

            int choiceIndex = i;
            choiceButtons[i].onClick.AddListener(() => ChooseChoice(choiceIndex));
        }
    }

    // uses the first button as a continue button
    private void SetupContinueButton()
    {
        if (choiceButtons.Length == 0 || choiceButtonTexts.Length == 0)
        {
            return;
        }

        if (choiceButtons[0] == null || choiceButtonTexts[0] == null)
        {
            return;
        }

        // let the player step through the back and forth lines
        choiceButtons[0].gameObject.SetActive(true);
        choiceButtonTexts[0].text = "Continue";
        choiceButtons[0].onClick.RemoveAllListeners();
        choiceButtons[0].onClick.AddListener(AdvanceDialogue);
    }

    // hides every choice button in the list
    private void HideAllChoices()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (choiceButtons[i] != null)
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // sets up the end conversation button
    private void SetupCloseButton()
    {
        int closeButtonIndex = GetCloseButtonIndex();

        if (closeButtonIndex < 0)
        {
            return;
        }

        if (choiceButtons[closeButtonIndex] == null || choiceButtonTexts[closeButtonIndex] == null)
        {
            return;
        }

        // turn on the close button every time dialogue is open
        choiceButtons[closeButtonIndex].gameObject.SetActive(true);
        choiceButtonTexts[closeButtonIndex].text = "End Conversation";
        choiceButtons[closeButtonIndex].onClick.RemoveAllListeners();
        choiceButtons[closeButtonIndex].onClick.AddListener(EndDialogue);
    }

    // finds the last valid button to use for ending dialogue
    private int GetCloseButtonIndex()
    {
        int maxIndex = Mathf.Min(choiceButtons.Length, choiceButtonTexts.Length) - 1;

        for (int i = maxIndex; i >= 0; i--)
        {
            if (choiceButtons[i] != null && choiceButtonTexts[i] != null)
            {
                return i;
            }
        }

        return -1;
    }

    // keeps one button reserved for ending the conversation
    private int GetChoiceButtonLimit()
    {
        int closeButtonIndex = GetCloseButtonIndex();

        if (closeButtonIndex <= 0)
        {
            return 0;
        }

        return closeButtonIndex;
    }

    // logs ink runtime problems in a simple way
    private void HandleStoryError(string message, Ink.ErrorType errorType)
    {
        // warn in the console so bad branches are easier to spot
        Debug.LogWarning(message);
    }

    // tells ink which choice player selected
    private void ChooseChoice(int choiceIndex)
    {
        if (_currentStory == null)
        {
            return;
        }

        if (choiceIndex < 0 || choiceIndex >= _currentStory.currentChoices.Count)
        {
            return;
        }

        // send selected choice back to ink
        _currentStory.ChooseChoiceIndex(choiceIndex);

        // move into the next line after the branch choice
        AdvanceDialogue();
    }

    // allows ink to call C# functions inside ink file
    private void BindExternalDialogueFunctions()
    {
        if (_currentStory == null)
        {
            return;
        }

        if (_pendingApprenticeLightDialogue == null)
        {
            return;
        }

        ApprenticeLightDialogue apprenticeLightDialogue = _pendingApprenticeLightDialogue;

        for (int i = 1; i <= apprenticeLightDialogue.GetLightOptionCount(); i++)
        {
            int lightIndex = i;

            // bind one ink function per inspector light slot
            _currentStory.BindExternalFunction("fix_light_" + lightIndex, () => apprenticeLightDialogue.TurnOnLight(lightIndex));
        }

        _pendingApprenticeLightDialogue = null;
    }
}
