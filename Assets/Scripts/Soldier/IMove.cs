using UnityEngine;

public interface IMove {

    void Move(Vector2 endPoint,SoldierBehaviours.UnitBehaviour nextState=SoldierBehaviours.UnitBehaviour.Idle);


}
