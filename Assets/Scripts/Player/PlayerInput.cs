using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public static bool OpenMenu() => Input.GetKeyDown(KeyCode.Escape);
    public static bool OpenTabMenu() => Input.GetKey(KeyCode.Tab);
}
