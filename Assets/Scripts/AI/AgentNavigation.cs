using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentNavigation : MonoBehaviour
{
    public bool PlayerInRange;
    public bool PlayerSeen;

    public bool Navigating;

    public NavMeshAgent Agent;

    Transform PlayerTransform = null;

    public RoomLists Spawner;

    //public Transform Visual;

    public GameObject CurrentlyHitting;

    public LayerMask VisionLayers;

	public void Update()
	{
        if (PlayerInRange) 
        {
            Vector3 lookRot = (transform.position - PlayerTransform.position).normalized;
            lookRot.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookRot);
            transform.localRotation = rotation;
        }
	}

	public void FixedUpdate()
	{
        if (PlayerInRange)
        {
            

            RaycastHit hit;

            if (Physics.Raycast(transform.position, (PlayerTransform.position - transform.position).normalized, out hit, 15f,VisionLayers))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    PlayerSeen = true;
                    
                }
                else
                {
                    PlayerSeen = false;
                }
                CurrentlyHitting = hit.collider.gameObject;
            }

            if (PlayerSeen)
            {
                StartCoroutine(MoveTo(PlayerTransform.position));
            }



        }
        else if (!Navigating && Spawner != null)
        {
            StartCoroutine( MoveTo(Spawner.RoomPoses[Random.Range(0,Spawner.RoomPoses.Count-1)]));;
        }
	}

    public IEnumerator MoveTo(Vector3 point) 
    {
        float destinationRange = 3;
        Navigating = true;

        Agent.ResetPath();

        Agent.SetDestination(point);

        while (Vector3.Distance(transform.position, point ) > destinationRange) 
        {
            //Debug.Log($"path status = {Agent.path.status}");
            yield return new WaitForEndOfFrame();
        }
        Navigating = false;
    }

	public void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInRange = true;
            PlayerTransform = other.transform;
        }
    }
	public void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("Player")) 
        {
            PlayerInRange = false;
        }
    }

	public void OnDrawGizmos()
	{
        Gizmos.color = Color.magenta;

        if (PlayerInRange) 
        {
            Debug.DrawRay(transform.position, ( PlayerTransform.position - transform.position).normalized *15f,Color.magenta);
        }
	}

}
