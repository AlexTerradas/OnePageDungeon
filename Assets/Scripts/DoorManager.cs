using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("Top Door")]
    [SerializeField]
    private DoorTrigger m_frontTriggerTop;

    [SerializeField]
    private DoorTrigger m_backTriggerTop;

    [SerializeField]
    private Canvas m_bubbleCanvasTop;

    [Header("Top Bottom")]
    [SerializeField]
    private DoorTrigger m_frontTriggerBottom;

    [SerializeField]
    private DoorTrigger m_backTriggerBottom;

    [SerializeField]
    private Canvas m_bubbleCanvasBottom;

    [Header("Animation")]
    [SerializeField]
    private Animation m_animation;
    private string m_animationNameTop;
    private string m_animationNameBottom;

    private bool m_animationPlayedTop;
    private bool m_animationPlayedBottom;

    [Header("GameManager")]
    [SerializeField]
    private GameManagerCSharp m_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_animationNameTop = "OpenDoorTop";
        m_animationNameBottom = "OpenDoorBottom";

        m_animationPlayedTop = false;
        m_animationPlayedBottom = false;

        m_bubbleCanvasTop.gameObject.SetActive(false);
        m_bubbleCanvasBottom.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // GAME MANAGER
        
        if (m_gameManager.GetPortaTop())
        {
            if (!m_animationPlayedTop)
            {
                DoorTop();
            }
        }

        if (m_gameManager.GetPortaBottom())
        {
            if (!m_animationPlayedBottom)
            {
                DoorBottom();
            }
        }

        // DOOR TOP

        if (m_backTriggerTop.GetPlayerInTrigger())
        {
            if (!m_animationPlayedTop)
            {
                DoorTop();
            }
        }

        if (m_frontTriggerTop.GetPlayerInTrigger())
        {
            m_bubbleCanvasTop.gameObject.SetActive(true);
        }
        else
        {
            m_bubbleCanvasTop.gameObject.SetActive(false);
        }

        // BOTTOM DOOR

        if (m_backTriggerBottom.GetPlayerInTrigger())
        {
            if (!m_animationPlayedBottom)
            {
                DoorBottom();
            }
        }

        if (m_frontTriggerBottom.GetPlayerInTrigger())
        {
            m_bubbleCanvasBottom.gameObject.SetActive(true);
        } else
        {
            m_bubbleCanvasBottom.gameObject.SetActive(false);
        }
    }

    public void DoorTop()
    {
        m_animation.Play(m_animationNameTop);
        m_frontTriggerTop.gameObject.SetActive(false);
        m_backTriggerTop.gameObject.SetActive(false);
        m_animationPlayedTop = true;
        m_gameManager.SetPortaTop(true);
    }

    public void DoorBottom()
    {
        m_animation.Play(m_animationNameBottom);
        m_frontTriggerBottom.gameObject.SetActive(false);
        m_backTriggerBottom.gameObject.SetActive(false);
        m_animationPlayedBottom = true;
        m_gameManager.SetPortaBottom(true);
    }
}
