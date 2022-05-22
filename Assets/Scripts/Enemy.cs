using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private enum State { IDLE, ATTACK };
    private GameObject player;
    private BoxCollider hurtBox;
    private NavMeshAgent agent;
    public float speed = 3.5f;
    State currentState;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hurtBox = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.IDLE;
    }

    private void OnEnable()
    {
        currentState = State.IDLE;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().movement = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.IDLE:
                IdleUpdate();
                break;
            case State.ATTACK:
                AttackUpdate();
                break;
        }
    }

    void IdleUpdate()
    {
        if (player.transform.position.y < transform.position.y + 0.2 && player.transform.position.y > transform.position.y -0.2)
        {
            ChangeState(State.ATTACK);
        }
    }

    void AttackUpdate()
    {
        if (player.transform.position.y > transform.position.y + 0.2 || player.transform.position.y < transform.position.y - 0.2)
        {
            ChangeState(State.IDLE);
        }

        if (player.GetComponent<PlayerMovement>().isRotating)
        {
            agent.speed = speed / player.GetComponent<PlayerMovement>().rotationSpeedXD;
        }
        else
        {
            agent.speed = speed;
        }
        agent.SetDestination(player.transform.position);
    }

    void ChangeState(State newState)
    {
        //OnExit
        switch (currentState)
        {
            case State.ATTACK:
                break;
            case State.IDLE:                
                break;
        }
        //OnEnter
        switch (newState)
        {
            case State.ATTACK:
                hurtBox.enabled = false;
                break;
            case State.IDLE:
                hurtBox.enabled = true;
                break;
        }
        currentState = newState;
    }
}
