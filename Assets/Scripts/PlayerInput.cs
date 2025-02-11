using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public static bool OpenMenu() => Input.GetKeyDown(KeyCode.Escape);
    public static bool Aim() => Input.GetMouseButton(1);
    public static bool Shoot() => Input.GetMouseButton(0);
    public static bool SwitchWeaponUp() => Input.mouseScrollDelta.y > 0f;
    public static bool SwitchWeaponDown() => Input.mouseScrollDelta.y < 0f;
    public static bool TakeFirstWeapon() => Input.GetKeyDown(KeyCode.Alpha1);
    public static bool TakeSecondWeapon() => Input.GetKeyDown(KeyCode.Alpha2);
    public static bool Reload() => Input.GetKeyDown(KeyCode.R);
}
