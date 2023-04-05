using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMoving : MonoBehaviour
{
    private Camera cam;
    private GameObject selected;
  
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

            if (selected != null)
            {
                Color selectColor = selected.GetComponent<SpriteRenderer>().color;
                selectColor.r =1f; selectColor.g = 1f; selectColor.b = 1f;
                selected.GetComponent<SpriteRenderer>().color = selectColor;

            }
                Collider2D soldier = Physics2D.OverlapCircle(mousePosition, 0.5f, LayerMask.GetMask("Soldier"));
                if (soldier != null)
                {
                    selected = soldier.gameObject;
                    Color selectColor = soldier.gameObject.GetComponent<SpriteRenderer>().color;
                    selectColor.r = 0.50f; selectColor.g = 0.50f; selectColor.b = 0.50f;
                    soldier.gameObject.GetComponent<SpriteRenderer>().color = selectColor;


                }
            
            else
            {
                selected = null;
            }
        }
        else if (Input.GetMouseButtonDown(1)&&selected!=null)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
            Vector3 movePosition = mousePosition;

            SoldierBehaviours.UnitBehaviour nextState = SoldierBehaviours.UnitBehaviour.Idle;

            Collider2D enemy = Physics2D.OverlapCircle(mousePosition, 0.5f, LayerMask.GetMask("Build","Soldier"));
            
            if (enemy!=null && selected.TryGetComponent(out IAttack attack))
            {
                Bounds buildingBounds=enemy.bounds;
                attack.SetEnemy(enemy.GetComponent<Damageable>());
                nextState = SoldierBehaviours.UnitBehaviour.Attack;
                buildingBounds.extents += Vector3.one*0.5f;
                movePosition = buildingBounds.ClosestPoint(selected.transform.position);
                
            }
           

            movePosition = new Vector3(Mathf.Ceil(movePosition.x), Mathf.Ceil(movePosition.y), Mathf.Ceil(movePosition.z));
            selected.GetComponent<IMove>().Move(movePosition,nextState);       




        }
    }




}
