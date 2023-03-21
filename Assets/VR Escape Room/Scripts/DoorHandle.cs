using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : XRBaseInteractable
{
    public Transform DraggedTransform; // set to parent door object
    public Vector3 LocalDragDirection; // set to -1, 0, 0
    public float DragDistance; // set to 0.8
    public int DoorWeight = 20;
    public GameObject canvas;

    private Vector3 m_StartPosition;
    private Vector3 m_EndPosition;
    private Vector3 m_WorldDragDirection;

    // Start is called before the first frame update
    private void Start()
    {
        m_WorldDragDirection = transform.TransformDirection(LocalDragDirection).normalized;

        m_StartPosition = DraggedTransform.position;
        m_EndPosition = m_StartPosition + m_WorldDragDirection * DragDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (isSelected)
        {
            var interactorTransform = firstInteractorSelecting.GetAttachTransform(this);
            Vector3 selfToInteractor = interactorTransform.position - transform.position;

            // calculate dot product of selfToInteractor onto drag direction
            float forceInDirectionOfDrag = Vector3.Dot(selfToInteractor, m_WorldDragDirection);

            //we then need to check in which direction are we dragging : toward the end (positive direction) or toward
            //the start (megative direction)
            bool dragToEnd = forceInDirectionOfDrag > 0.0f;

            // calculate speed based the dot product
            //we take the absolute of that value now, as we need a speed, not a direction anymore
            float absoluteForce = Mathf.Abs(forceInDirectionOfDrag);

            //we transform our force into a speed (by dividing it by delta Time). Then we "scale" that speed by the door
            //weight. The "heavier" the door, the lower the speed will be.
            float speed = absoluteForce / Time.deltaTime / DoorWeight;

            // move door based on speed using MoveTowards
            //finally we move the target either toward end or start based on the speed.
            DraggedTransform.position = Vector3.MoveTowards(DraggedTransform.position,
                //the target depend on the direction of drag we recovered earlier
                dragToEnd ? m_EndPosition : m_StartPosition,speed * Time.deltaTime);

            canvas.SetActive(true);
        }
    }


}
