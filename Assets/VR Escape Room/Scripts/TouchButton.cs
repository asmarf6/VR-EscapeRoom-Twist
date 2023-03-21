using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TouchButton : XRBaseInteractable
{
    [Header("Button Data")]
    public int buttonNumber;

    private MeshRenderer mr;

    [Header("Materials")]
    public Material TouchedMaterial;
    public Material NormalMaterial;

    public GameObject LinkedKeypad;
    private NumberPad numpad;

    // Start is called before the first frame update
    void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
        numpad = LinkedKeypad.GetComponent<NumberPad>();
    }


    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        mr.material = TouchedMaterial;
        numpad.ButtonPressed(buttonNumber);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        mr.material = NormalMaterial;
    }
}
