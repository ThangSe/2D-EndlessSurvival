using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwarInverted,
    }

    [SerializeField] private Mode mode;

    private void Start()
    {
        Player.Instance.OnFlipAction += Player_OnFlipAction;
    }

    private void Player_OnFlipAction(object sender, System.EventArgs e)
    {
        if(Player.Instance.transform.localScale.x == 1)
        {
            mode = Mode.CameraForward;
        }
        if(Player.Instance.transform.localScale.x == -1)
        {
            mode = Mode.CameraForwarInverted;
        }
    }

    private void Update()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwarInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
