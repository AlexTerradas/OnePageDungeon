using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCSharp : MonoBehaviour
{
    private static GameManagerCSharp m_instance;

    [SerializeField]
    private bool portaTop;
    [SerializeField]
    private bool portaBottom;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy1;
    [SerializeField]
    private GameObject enemy2;

    private Vector3 startPosPlayer;
    private Vector3 startPosEnemy1;
    private Vector3 startPosEnemy2;

    private Quaternion startRotPlayer;
    private Quaternion startRotEnemy1;
    private Quaternion startRotEnemy2;

    public bool GetPortaTop()
    {
        return portaTop;
    }

    public bool GetPortaBottom()
    {
        return portaBottom;
    }

    public void SetPortaTop(bool res)
    {
        portaTop = res;
    }

    public void SetPortaBottom(bool res)
    {
        portaBottom = res;
    }

    private void Awake()
    {
        portaTop = false;
        portaBottom = false;

        // player
        startPosPlayer = player.transform.position;
        startRotPlayer = player.transform.rotation;

        // enemy
        startPosEnemy1 = enemy1.transform.position;
        startRotEnemy1 = enemy1.transform.rotation;

        // enemy2
        startPosEnemy2 = enemy2.transform.position;
        startRotEnemy2 = enemy2.transform.rotation;
    }

    public void Restart()
    {
        // player
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = startPosPlayer;
        player.transform.rotation = startRotPlayer;
        player.GetComponent<CharacterController>().enabled = true;

        // enemy
        enemy1.SetActive(false);
        enemy1.transform.position = startPosEnemy1;
        enemy1.transform.rotation = startRotEnemy1;
        enemy1.SetActive(true);

        // enemy2
        enemy2.SetActive(false);
        enemy2.transform.position = startPosEnemy2;
        enemy2.transform.rotation = startRotEnemy2;
        enemy2.SetActive(true);


    }
}
