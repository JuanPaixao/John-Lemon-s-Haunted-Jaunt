using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public bool isPlayerInRange;
    public RaycastHit raycastHit;
    public GameEnding gameEnding;
    private void Update()
    {
        if (isPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up; // To make sure the Observer can see JohnLemon’s centre of mass, you’re pointing the
                                                                                   //  direction up one unit by adding Vector3.up. Vector3.up is a shortcut for (0, 1, 0).  
            Ray ray = new Ray(transform.position, direction);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }
}
