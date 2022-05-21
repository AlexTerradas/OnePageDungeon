using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleted : MonoBehaviour
{

    [SerializeField]
    private Canvas m_canvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_canvas.gameObject.SetActive(true);
            other.gameObject.SetActive(false);
        }
    }
}
