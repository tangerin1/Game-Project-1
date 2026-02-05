using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    // shoots out a fireball
    public GameObject fireballPrefab;

    // mouth of dragon
    public Transform firePoint;

    public float health = 200f;

    [Header("Movement Settings")]
    public float radius = 10f;
    public float moveSpeed = 45f; // turns how many degrees per second?
    public Transform pivotPoint;  // center of the level, the dragon turns around it

    [Header("Attack Settings")]
    public float fireballSpeed = 20f; // speed of the fireball
    public float waitTime = 1f; // waits a bit when making moves, the player can attack!

    // boss randomly decides to move left or right in its logic
    private bool movingRight = true;
    private float currentAngle = 0f;

    private float startHeight;

    public HealthBar healthBar;


    void Start()
    {

        startHeight = transform.position.y;

        // Initialize angle based on starting position
        Vector3 offset = transform.position - pivotPoint.position;
        currentAngle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;

        StartCoroutine(DragonLoop());
    }

    IEnumerator DragonLoop()
    {
        while (true)
        {
            yield return StartCoroutine(MoveHalfCircle());
            yield return new WaitForSeconds(waitTime);
            ShootFireball();
        }
    }

    IEnumerator MoveHalfCircle()
    {
        movingRight = Random.value > 0.5f;

        float targetAngle = currentAngle + (movingRight ? 180f : -180f);

        float direction;

        while (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 1f)
        {
            if (movingRight)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            currentAngle += direction * moveSpeed * Time.deltaTime;

            float rad = currentAngle * Mathf.Deg2Rad;

            Vector3 newPos = new Vector3(
                pivotPoint.position.x + Mathf.Cos(rad) * radius,
                startHeight,
                pivotPoint.position.z + Mathf.Sin(rad) * radius
            );
            transform.position = newPos;

            // Face player
            Vector3 lookDir = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(lookDir);

            yield return null;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        // update health bar
        healthBar.TakeDamage(amount);
        Debug.Log("Dragon health: " + health);
        Debug.Log("HealthBar current: " + healthBar.currentHealth);
        
        if (health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    void ShootFireball()
    {
        GameObject fb = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = fb.GetComponent<Rigidbody>();
        rb.linearVelocity = (player.position - firePoint.position).normalized * fireballSpeed;

        Destroy(fb, 2f);

    }
}
