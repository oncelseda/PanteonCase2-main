using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMoving : MonoBehaviour
{
    private Camera cam;
    private IMove move;
  
    private void Start()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
            mousePosition = new Vector3(Mathf.Ceil(mousePosition.x), Mathf.Ceil(mousePosition.y), Mathf.Ceil(mousePosition.z));

            Collider2D soldier = Physics2D.OverlapCircle(mousePosition, 0.5f,LayerMask.GetMask("Soldier"));
            if (soldier != null && soldier.TryGetComponent(out IMove m))
            {
                move = m;
                
            }
            else
            {
                move = null;
            }
        }
        else if (Input.GetMouseButtonDown(1)&&move!=null)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
            mousePosition = new Vector3(Mathf.Ceil(mousePosition.x), Mathf.Ceil(mousePosition.y), Mathf.Ceil(mousePosition.z));

            //Bina var mý yok mu kontrol et!!!
            move.Move(mousePosition);       

        }
    }




}
