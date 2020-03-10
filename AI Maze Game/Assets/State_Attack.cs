using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Attack: IState
{
    EnemyController owner;
    NavMeshAgent agent;

    public State_Attack(EnemyController owner) {this.owner = owner;}

    public void Enter()
    {
        //if they are entering the attack state, find the last seen location and start moving if stopped
        Debug.Log("entering attack state");
        agent = owner.GetComponent<NavMeshAgent>();
        if(owner.seenTarget)
        {
            agent.destination = owner.lastSeenPosition;
            agent.isStopped = false;
        }
    }

    public void Execute()
    {
        //find path to player and move there
        //Debug.Log("updating attack state");
        agent.destination = owner.lastSeenPosition;
        agent.isStopped = false;

        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.isStopped = true;
        }

        if(owner.seenTarget != true)
        {
            Debug.Log("lost sight");
            //change state to search for the player
            owner.stateMachine.ChangeState(new State_Searching(owner));
        }

        //fire on the player ...
    }

    public void Exit()
    {
        Debug.Log("exiting attack state");
        agent.isStopped = true;
    }
}
