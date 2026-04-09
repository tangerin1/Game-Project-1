using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

/*
runs the apprentice light choice interaction
opens an ink dialogue and lets ink trigger the chosen light
*/
public class ApprenticeLightDialogue : MonoBehaviour
{
    // dialogue manager that shows the panel
    public InkDialogueManager dialogueManager;

    // navmesh agent that walks apprentice 
    public NavMeshAgent agent;

    // the authored apprentice ink json file
    public TextAsset inkJson;

    // key used to open the apprentice dialogue
    public Key talkKey = Key.F;

    // the spotlights that can be turned on
    public Light lightOne;
    public Light lightTwo;
    public Light lightThree;

    // the positions for each light to walk to
    public Transform lightOneTarget;
    public Transform lightTwoTarget;
    public Transform lightThreeTarget;

    // how close the apprentice has to get before the light turns on
    public float arriveDistance = 0.4f;

    private Light _pendingLight;
    private bool _isWalkingToLight;

    private void Start()
    {
        if (dialogueManager == null)
        {
            dialogueManager = FindFirstObjectByType<InkDialogueManager>();
        }

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    // listens for apprentice talk key
    private void Update()
    {
        CheckForArrival();

        if (dialogueManager == null)
        {
            return;
        }

        // do not open another dialogue on top of one that is already running
        if (dialogueManager.IsDialogueOpen)
        {
            return;
        }

        if (Keyboard.current == null)
        {
            return;
        }

        // open the apprentice ink dialogue when key is pressed
        if (Keyboard.current[talkKey].wasPressedThisFrame)
        {
            OpenDialogue();
        }
    }

    // opens the apprentice ink dialogue
    private void OpenDialogue()
    {
        if (inkJson == null)
        {
            return;
        }

        // tell the manager this next ink story should be allowed to call apprentice light functions
        dialogueManager.SetApprenticeLightDialogue(this);
        dialogueManager.StartDialogue(inkJson);
    }

    // lets ink choose which of the three lights to turn on
    public void TurnOnLight(int lightIndex)
    {
        if (lightIndex == 1)
        {
            TurnOnChosenLight(lightOne, lightOneTarget);
            return;
        }

        if (lightIndex == 2)
        {
            TurnOnChosenLight(lightTwo, lightTwoTarget);
            return;
        }

        if (lightIndex == 3)
        {
            TurnOnChosenLight(lightThree, lightThreeTarget);
        }
    }

    // turns on a specific light given a light object and position to walk to
    private void TurnOnChosenLight(Light targetLight, Transform targetPoint)
    {
        if (targetLight == null)
        {
            return;
        }

        if (agent == null || targetPoint == null)
        {
            TurnOnAssignedLight(targetLight);
            return;
        }

        _pendingLight = targetLight;
        _isWalkingToLight = true;

        // send the apprentice to selected spotlight position
        agent.isStopped = false;
        agent.SetDestination(targetPoint.position);
    }

    // has the apprentice reached the chosen light
    private void CheckForArrival()
    {
        if (!_isWalkingToLight)
        {
            return;
        }

        if (agent == null)
        {
            _isWalkingToLight = false;
            TurnOnAssignedLight(_pendingLight);
            return;
        }

        if (agent.pathPending)
        {
            return;
        }

        if (agent.remainingDistance > arriveDistance)
        {
            return;
        }

        if (agent.hasPath && agent.velocity.sqrMagnitude > 0.01f)
        {
            return;
        }

        // stop moving once they are close enough
        agent.isStopped = true;
        _isWalkingToLight = false;
        TurnOnAssignedLight(_pendingLight);
        _pendingLight = null;
    }

    private void TurnOnAssignedLight(Light targetLight)
    {
        if (targetLight == null)
        {
            return;
        }

        Transform currentTransform = targetLight.transform;

        while (currentTransform != null)
        {
            currentTransform.gameObject.SetActive(true);
            currentTransform = currentTransform.parent;
        }

        // turn light component on
        targetLight.enabled = true;
    }
}
