using UnityEngine;
public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepClips;

    [SerializeField] private float minInterval = 0.4f;
    [SerializeField] private float maxInterval = 0.8f;

    private float timer;
    private float nextStepTime;
    private int lastIndex = -1;

    void Start()
    {
        SetNextStepTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextStepTime)
        {
            PlayRandomFootstep();
            SetNextStepTime();
            timer = 0f;
        }
    }

    private void SetNextStepTime()
    {
        nextStepTime = Random.Range(minInterval, maxInterval);
    }

    private void PlayRandomFootstep()
    {
        if (footstepClips.Length == 0) return;

        int index;
        do
        {
            index = Random.Range(0, footstepClips.Length);
        }
        while (index == lastIndex);

        lastIndex = index;
        audioSource.PlayOneShot(footstepClips[index]);
    }
}