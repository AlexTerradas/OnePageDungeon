using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    [SerializeField]
    private DoorTrigger m_frontTrigger;

    [SerializeField]
    private DoorTrigger m_backTrigger;

    [SerializeField]
    private DoorTrigger m_frontTriggerUp;

    [SerializeField]
    private DoorTrigger m_backTriggerUp;

    [SerializeField]
    private Animation m_animation;
    private string m_animationName;

    private bool m_animationPlayed;

    [SerializeField]
    private Canvas m_bubbleCanvas;
    [SerializeField]
    private Canvas m_bubbleCanvasUp;

    // Start is called before the first frame update
    void Start()
    {
        m_animationName = "OpenDoor";
        m_animationPlayed = false;
        m_bubbleCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_backTrigger.GetPlayerInTrigger())
        {
            if(!m_animationPlayed)
            {
                m_animation.Play(m_animationName);
                m_frontTrigger.gameObject.SetActive(false);
                m_backTrigger.gameObject.SetActive(false);
                m_animationPlayed = true;
            }
        }

        if(m_frontTrigger.GetPlayerInTrigger())
        {
            m_bubbleCanvas.gameObject.SetActive(true);
        } else
        {
            m_bubbleCanvas.gameObject.SetActive(false);
        }

        if (m_frontTriggerUp.GetPlayerInTrigger())
        {
            m_bubbleCanvasUp.gameObject.SetActive(true);
        }
        else
        {
            m_bubbleCanvasUp.gameObject.SetActive(false);
        }
    }
}
