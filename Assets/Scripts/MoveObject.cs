using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveObject : MonoBehaviour
{
    private SpriteRenderer moveSprite;
    private Camera cam;
    private int collidingCount = 0;
    public bool destroyRB;


    private void Update()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
        Move(mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (collidingCount == 0)
            {
                Place();
            }
        }
    }
    public void Initialize(SpriteRenderer sr,bool destroyRB=false)
    {

        moveSprite = sr;
        cam = Camera.main;
        this.destroyRB = destroyRB;
    }
    public void Move(Vector3 position)
    {
        position = new Vector3(Mathf.Floor(position.x), Mathf.Floor(position.y), Mathf.Floor(position.z));
        transform.position = position;
    }

    public void Place()
    {

        Bounds buildBound = GetComponent<Collider2D>().bounds;
        buildBound.extents -= Vector3.one *0.5f;
        BuildingFactory.instance.gameBoard.SetWalkable(buildBound,false);
      

        Destroy(this);
        if (destroyRB == true) {

            Destroy(GetComponent<Rigidbody2D>());
        }
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidingCount++;
        if (collidingCount != 0)
        {
            moveSprite.color = Color.red;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collidingCount--;
        if (collidingCount == 0)
        {
            moveSprite.color = Color.white;
        }

    }

}
