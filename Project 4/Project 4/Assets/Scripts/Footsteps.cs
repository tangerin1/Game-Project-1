using UnityEngine;
public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private float stepInterval = 0.4f; // intervals for each footstep

    private float timer;
    private int lastIndex = -1;

    void Update()
    {
        bool isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

        if (isMoving)
        {
            timer += Time.deltaTime;
            if (timer >= stepInterval)
            {
                PlayRandomFootstep();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
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