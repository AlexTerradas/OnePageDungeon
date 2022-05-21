using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCSharp : MonoBehaviour
{
    private static GameManagerCSharp m_instance;

    [SerializeField]
    private bool porta1;
    [SerializeField]
    private bool porta2;

    public bool GetPorta1()
    {
        return porta1;
    }

    public bool GetPorta2()
    {
        return porta2;
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }
        else if (m_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        porta1 = false;
        porta2 = false;
    }
}
