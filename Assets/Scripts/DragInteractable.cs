using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public class DragEvent : UnityEvent<float> { }

public class DragInteractable : XRBaseInteractable
{
    public Transform startDragPosition = null;
    public Transform endDragPosition = null;

    [HideInInspector]
    public float dragPercentage = 0.0f;

    protected XRBaseInteractor m_interactor = null;

    public UnityEvent onDragStart = new UnityEvent();
    public UnityEvent onDragEnd = new UnityEvent();
    public DragEvent onDragUpdate = new DragEvent();


    Coroutine m_drag = null;

    void StartDrag()
    {
        Debug.Log("StartDrag");
        if (m_drag != null)
        {
            StopCoroutine(m_drag);
        }

        m_drag = StartCoroutine(CalculateDrag());
        onDragStart?.Invoke();
    }

    void EndDrag()
    {
        Debug.Log("EndDrag");
        if (m_drag != null)
        {
            StopCoroutine(m_drag);
            m_drag = null;
            onDragEnd?.Invoke();
        }
    }


    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;

        //the dot of a to value divided by the dot of the total range
        //gives the normalized 0-1 distance of value between a and b
        return Mathf.Clamp01(Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB));
    }

    IEnumerator CalculateDrag()
    {
        while(m_interactor != null)
        {
            //get a line in local
            Vector3 line = startDragPosition.localPosition - endDragPosition.localPosition;

            //convert our interactor position to local space
            Vector3 interactorLocalPosition = startDragPosition.parent.InverseTransformPoint(m_interactor.transform.position);

            //project the intractor position onto the line
            Vector3 projectedPoint = Vector3.Project(interactorLocalPosition, line.normalized);

            //reverse interpolate that position on the line to get a percentage of how far the drag has moved
            dragPercentage = InverseLerp(startDragPosition.localPosition, endDragPosition.localPosition, projectedPoint);
            Debug.Log("dragPercentage >: "+ dragPercentage);
            onDragUpdate.Invoke(dragPercentage);
            yield return null;
        }


    }

    //protected override void OnSelectEntered(XRBaseInteractor interactor)//(SelectEnterEventArgs args)
    protected override void OnSelectEntered(SelectEnterEventArgs interactor)
    {
        Debug.Log("OnSelectEntered: " + interactor);
        base.OnSelectEntered(interactor);
        m_interactor = (UnityEngine.XR.Interaction.Toolkit.XRBaseInteractor)interactor.interactorObject;
        StartDrag();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log("OnSelectExited: " + interactor);
        //base.OnSelectEntered(args);
        EndDrag();
        m_interactor = null;
        base.OnSelectExited(interactor);
    }
}
