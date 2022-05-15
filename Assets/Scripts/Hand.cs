using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum HandType
{
    Left,
    Right 
};

public class Hand : MonoBehaviour
{
    public HandType type = HandType.Left;
    public bool isHidden { get; private set; } = false;
    public InputAction trackedAction = null;

    public InputAction gripAction = null;
    public Animator handAnimator = null;
    int m_gripAmountParameter = 0;


    public InputAction triggerAction = null;
    //public Animator handAnimator = null;
    int m_pointAmountParameter = 0;

    bool m_isCurrentlyTracked = false;

    List<Renderer> m_currentRenderers = new List<Renderer>();

    Collider[] m_colliders = null;
    List<Collider>  colliderList = new List<Collider>();
    public bool isCollisionEnabled { get; private set; } = false;
    // Start is called before the first frame update
    void Start()
    {

        foreach (Collider childCollider in GetComponentsInChildren<Collider>())
        {
            if (!childCollider.isTrigger)
                colliderList.Add(childCollider);
        }

        m_colliders = colliderList.ToArray();

        trackedAction.Enable();
        m_gripAmountParameter = Animator.StringToHash("GripAmount");
        m_pointAmountParameter = Animator.StringToHash("PointAmount");
        gripAction.Enable();
        triggerAction.Enable();

        Hide();
    }

    void UpdateAnimations()
    {
        float pointAmount = triggerAction.ReadValue<float>();
        Debug.Log("UpdateAnimations() - pointAmount: " + pointAmount);
        handAnimator.SetFloat(m_pointAmountParameter, pointAmount);

        float gripAmount = gripAction.ReadValue<float>();
        Debug.Log("UpdateAnimations() - gripAmount: " + gripAmount);
        handAnimator.SetFloat(m_gripAmountParameter, Mathf.Clamp01(gripAmount + pointAmount));
    }

    // Update is called once per frame
    void Update()
    {
        float isTracked = trackedAction.ReadValue<float>();
        if(isTracked == 1.0f && !m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = true;
            Show(); 
        }
        else if(isTracked == 0 && m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = false;
            Hide();
        }

        UpdateAnimations();
    }

   public void Hide()
    {
        m_currentRenderers.Clear();
        Renderer[] renderers = GetComponentsInChildren<Renderer>();


        foreach(Renderer renderer2 in renderers)
        {
            renderer2.enabled = false;
            m_currentRenderers.Add(renderer2);
        }

        isHidden = true;
        EnableCollisions(true);
    }

    public void Show()
    {
        foreach (Renderer renderer2 in m_currentRenderers)
        {
            renderer2.enabled = true;
        }

        isHidden = false;
        EnableCollisions(true);
    }

    public void EnableCollisions(bool enabled)
    {
        if (isCollisionEnabled == enabled)
            return;

        isCollisionEnabled = enabled;
        foreach(Collider collider2 in m_colliders)
        {
            collider2.enabled = isCollisionEnabled;
        }
    }
}
