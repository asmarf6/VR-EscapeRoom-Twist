using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class CardReader : XRSocketInteractor
{
    private Transform m_KeyCardTransform;
    private Vector3 m_HoverEntry;
    private bool m_SwipeIsValid = false;
    public GameObject doorLock;
    private float AllowedUprightErrorRange = 0.5f;

    [Header("Materials")]
    public Material InvalidMaterial;
    public Material ValidMaterial;
    public Material OriginalRed;
    public Material OriginalGreen;

    [Header("Lights")]
    public GameObject RedLight;
    public GameObject GreenLight;

    private MeshRenderer mr_RedLight;
    private MeshRenderer mr_GreenLight;


    // Start is called before the first frame update
    void Start()
    {
        mr_RedLight = RedLight.GetComponent<MeshRenderer>();
        mr_GreenLight = GreenLight.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_KeyCardTransform != null)
        {
            Vector3 keycardUp = m_KeyCardTransform.forward;
            float dot = Vector3.Dot(keycardUp, Vector3.up);
            Debug.Log(dot);

            if (dot < 1 - AllowedUprightErrorRange)
            {
                m_SwipeIsValid = false;
                mr_RedLight.material = InvalidMaterial;
            }
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        m_KeyCardTransform = args.interactableObject.transform;
        m_HoverEntry = m_KeyCardTransform.position;
        m_SwipeIsValid = true;


    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        base.CanSelect(interactable);
        return false;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        Vector3 entryToExit = m_KeyCardTransform.position - m_HoverEntry;

        if (m_SwipeIsValid && entryToExit.y < -0.15f)
        {

            doorLock.SetActive(false);
            mr_GreenLight.material = ValidMaterial;
            mr_RedLight.material = OriginalRed;
          //  HandleToEnable.enabled = true;
        }

        m_KeyCardTransform = null;

    }

}
