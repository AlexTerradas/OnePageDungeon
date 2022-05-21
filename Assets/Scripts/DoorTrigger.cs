using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private bool m_playerInTrigger;

    public bool GetPlayerInTrigger()
    {
        return m_playerInTrigger;
    }

    void Start()
    {
        m_playerInTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("player is in!");
            m_playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("player is out!");
            m_playerInTrigger = false;
        }
    }

}
