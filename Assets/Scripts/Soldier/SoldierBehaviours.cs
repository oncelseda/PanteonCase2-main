using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviours : MonoBehaviour, IMove
{


    private int currentTileIndex;
    private List<Vector3> pathVectorList;
    private float speed=3f;

    private void Update()
    {
        HandleMovement();
    }


    public void Move(Vector2 endPoint)
    {

        currentTileIndex = 0;
        pathVectorList = Pathfinding.instance.FindPath(transform.position, endPoint);
        if (pathVectorList != null && pathVectorList.Count > 1)
        {

            pathVectorList.RemoveAt(0);
        }


    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentTileIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.3f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

               
                Debug.DrawLine(transform.position,pathVectorList[pathVectorList.Count-1],Color.blue);
                transform.position = Vector3.MoveTowards(transform.position,targetPosition,speed*Time.deltaTime);
            }
            else
            {
                currentTileIndex++;
                if (currentTileIndex >= pathVectorList.Count)
                {
                    StopMoving();
                    
                }
            }
           
        }
    }
    private void StopMoving()
    {
        transform.position = pathVectorList[pathVectorList.Count-1];
        pathVectorList = null;
    }

}
