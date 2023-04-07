using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviours : Damageable, IMove, IAttack, ICounterAttack
{


    private int currentTileIndex;
    private List<Vector3> pathVectorList;
    private Damageable enemy;
    private UnitBehaviour currentState;
    private UnitBehaviour nextState;
    private IEnumerator attackRoutine;
    [SerializeField] private SoldierSO soldier;


    public enum UnitBehaviour
    {

        Idle,
        Attack,
        Move

    }

    private void Start()
    {
        currentHealth = soldier.SoldierMaxHealth;
        healthbar.SetHealth(currentHealth, soldier.SoldierMaxHealth);

    }

    private void Update()
    {
        HandleMovement();
    }

    private IEnumerator AttackCoroutine()
    {
        WaitForSeconds attackCooldown = new WaitForSeconds(soldier.AttackCooldown);

       
;      
            while (enemy.currentHealth != 0 && enemy != null)
            {
         
                Attack();
            
                yield return attackCooldown;
            }
        

    }

    private void Attack()
    {
      
        if (IsInNeighbour(enemy.transform.position) && enemy.GetComponent<SoldierBehaviours>()!=null) 
        {
            enemy.TakeDamage(soldier.Damage, this);
        }
        else if (enemy.GetComponent<SoldierBehaviours>() == null)
        {
            enemy.TakeDamage(soldier.Damage, this);
        }
       
    }

    public void Move(Vector2 endPoint, UnitBehaviour next)
    {
        StopAttack();
        currentTileIndex = 0;
        pathVectorList = Pathfinding.instance.FindPath(transform.position, endPoint);
        if (pathVectorList != null && pathVectorList.Count > 1)
        {

            pathVectorList.RemoveAt(0);
        }

        nextState = next;

    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentTileIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.3f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;


                Debug.DrawLine(transform.position, pathVectorList[pathVectorList.Count - 1], Color.blue);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, soldier.Speed * Time.deltaTime);
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
        transform.position = pathVectorList[pathVectorList.Count - 1];
        pathVectorList = null;
        currentState = nextState;

        if (currentState == UnitBehaviour.Attack && enemy != null && enemy.IsInNeighbour(transform.position))
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        StopAttack();
        attackRoutine = AttackCoroutine();
        StartCoroutine(attackRoutine);

    }
    private void StopAttack()
    {

        if (attackRoutine != null)
        {

            StopCoroutine(attackRoutine);

        }

    }

    public void SetEnemy(Damageable enemy)
    {
        if (this.enemy == enemy)
            return;
        this.enemy = enemy;

    }

    public void onAttacked(Damageable enemy)
    {
        if (this.enemy == enemy) { return; }
        SetEnemy(enemy);
        StartAttack();
        currentState = UnitBehaviour.Attack;

    }
}
