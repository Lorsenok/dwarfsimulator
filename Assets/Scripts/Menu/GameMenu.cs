using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    private bool closed = true;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private Menu blankMenu;
    [SerializeField] private string startMenu;

    [SerializeField] private Transform[] otherCanvas;
    [SerializeField] private float otherCanvasSpeed;
    [SerializeField] private Vector3 otherCanvasOpenPosition;
    [SerializeField] private Vector3 otherCanvasClosePosition;

    private void Update()
    {
        if (blankMenu.Open) closed = true;
        if (PlayerInput.OpenMenu())
        {
            closed = !closed;
            if (!closed)
            {
                menuManager.MenuOpen(startMenu);
                blankMenu.Open = false;
            }
        }
        if (closed) menuManager.MenuOpen(blankMenu.MenuName);

        foreach (Transform t in otherCanvas)
        {
            t.position = Vector3.Lerp(t.position, closed ? otherCanvasOpenPosition : otherCanvasClosePosition, Time.deltaTime * otherCanvasSpeed);
        }
    }
}
