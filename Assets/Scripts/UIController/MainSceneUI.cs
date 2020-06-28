using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUI : MonoBehaviour
{
    public Joystick joystick;
    private void OnEnable()
    {

    }

    private void FixedUpdate()
    {
        GameManager.Instance.JoystickMove(joystick.Horizontal, joystick.Vertical);
    }
}
