using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Teleporter : MonoBehaviour
{




    [SerializeField] private Camera playerCamera;
    public Transform[] teleportPoints;

    private bool beenSeen;

    //private Renderer rend;

    void Start()
    {
        //rend = GetComponent<Renderer>();
        beenSeen = false;
    }

    void Update()
    {

        
        bool currentlyVisible = isVisible();

        

        Debug.Log("Visible: " + currentlyVisible);

        if (currentlyVisible)
        {
            beenSeen = true; 
        }

        // teleport if not visible
        if (!currentlyVisible && beenSeen)
        {
            Teleport();
        } 
    }



    private bool isVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        return planes.All(planes => planes.GetDistanceToPoint(transform.position) >= 0);
    }

    void Teleport()
    {
        if (teleportPoints.Length == 0)
        {
            Debug.LogWarning("No teleport points assigned!");
            return;
        }

        int index = Random.Range(0, teleportPoints.Length);
        transform.position = teleportPoints[index].position;
    }
}