using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public static bool Closed { get; set; } = true;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private Menu blankMenu;
    [SerializeField] private string startMenu;

    [SerializeField] private Transform[] otherCanvas;
    [SerializeField] private float otherCanvasSpeed;
    [SerializeField] private Vector3 otherCanvasOpenPosition;
    [SerializeField] private Vector3 otherCanvasClosePosition;

    private void Update()
    {
        if (blankMenu.Open) Closed = true;
        if (PlayerInput.OpenMenu())
        {
            Closed = !Closed;
            if (!Closed)
            {
                menuManager.MenuOpen(startMenu);
                blankMenu.Open = false;
            }
        }
        if (Closed) menuManager.MenuOpen(blankMenu.MenuName);

        foreach (Transform t in otherCanvas)
        {
            t.position = Vector3.Lerp(t.position, Closed ? otherCanvasOpenPosition : otherCanvasClosePosition, Time.deltaTime * otherCanvasSpeed);
        }
    }
}
