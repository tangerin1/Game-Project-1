using UnityEngine;


// as part of the level, we will basically blast music to make the player feel they are watched.
public class BlastMusic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip soundToPlay;
    [SerializeField] private float delay = 60f;

    void Start()
    {
        StartCoroutine(PlayAfterDelay());
    }

    private System.Collections.IEnumerator PlayAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(soundToPlay);
    }
}