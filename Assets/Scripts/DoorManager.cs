using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private static DoorManager m_instance;

    [SerializeField]
    private DoorTrigger m_frontTrigger;

    [SerializeField]
    private DoorTrigger m_backTrigger;

    [SerializeField]
    private Animation m_animation;
    private string m_animationName;

    private bool m_animationPlayed;

    [SerializeField]
    private Canvas m_bubbleCanvas;

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

    // Start is called before the first frame update
    void Start()
    {
        m_animationName = "OpenDoor";
        m_animationPlayed = false;
        m_bubbleCanvas.enabled = false;
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
            m_bubbleCanvas.enabled = true;
        } else
        {
            m_bubbleCanvas.enabled = false;
        }
    }
}
