using UnityEngine;

public class BackAndForth : MonoBehaviour
{

    public float speed = 3f;
    public float distance = 3.0f;
    public bool direction = true; // true is right, false is left

    private UnityEngine.Vector3 start;
    private UnityEngine.Vector3 end;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start = transform.position;

        if (!direction)
        {
            distance = -distance;
        }
        
        end = start + new UnityEngine.Vector3(distance, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {


        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = UnityEngine.Vector3.Lerp(start, end, time);
    }
}
