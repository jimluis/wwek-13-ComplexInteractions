using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public class TurnEvent : UnityEvent<float> { }

public class TurnInteractable : XRBaseInteractable
{

    protected XRBaseInteractor m_interactor = null;
    Coroutine m_turn = null;
    public UnityEvent onTurnStart = new UnityEvent();
    public UnityEvent onTurnStop = new UnityEvent();
    public TurnEvent onTurnUpdate = new TurnEvent();

    Vector3 m_startingRotation = Vector3.zero;

    [HideInInspector]
    public float turnAngle = 0.0f;

    Quaternion GetLocalRoation(Quaternion targetWold)
    {
        return Quaternion.Inverse(targetWold) * transform.rotation;
    }

    void StartTurn()
    {
        if (m_turn != null)
        {
            StopCoroutine(m_turn);
        }
 
        Quaternion localRotation = GetLocalRoation(m_interactor.transform.rotation);
        m_startingRotation = localRotation.eulerAngles;
        onTurnStart?.Invoke();
        m_turn = StartCoroutine(UpdateTurn());
    }

    void StopTurn()
    {
        if (m_turn != null)
        {
            StopCoroutine(m_turn);
            onTurnStop?.Invoke();
            m_turn = null;
        }
    }

    IEnumerator UpdateTurn()
    {
        while(m_interactor != null)
        {
            Quaternion localRotation = GetLocalRoation(m_interactor.transform.rotation);
            turnAngle = m_startingRotation.z - localRotation.eulerAngles.z;
            onTurnUpdate?.Invoke(turnAngle);
            yield return null;
        }

    }

}
