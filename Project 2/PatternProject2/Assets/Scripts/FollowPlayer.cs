using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float delayTime = 10f;
    private bool follow = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartFollowing());
    }

    IEnumerator StartFollowing()
    {
        yield return new WaitForSeconds(delayTime);
        follow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!follow)
        {
            return;
        }

        float step = Time.deltaTime * speed;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }
}
