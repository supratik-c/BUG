using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTesterController : MonoBehaviour
{

    public Camera MainCam;

    public NavMeshAgent Tester;

    public LineRenderer LR;

    private void Awake() 
    {
        MainCam = Camera.main;
        Tester = GetComponent<NavMeshAgent>();
        LR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);

            
            if (Physics.Raycast(ray, out hit)) 
            {
                Tester.SetDestination(hit.point);
                DrawPath();
            }
        }
    }

    public void DrawPath() 
    {
        NavMeshPath path = Tester.path;

        LR.positionCount = path.corners.Length;

        LR.SetPosition(0, Tester.transform.position);

        for (int i = 1 ; i < path.corners.Length; i++) 
        {
            LR.SetPosition(i,path.corners[i]);
        }

        Debug.Log("Draw path called");
    }
}
