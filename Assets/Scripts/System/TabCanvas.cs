using UnityEngine;

public class TabCanvas : MonoBehaviour
{
    [SerializeField] private Menu blankMenu;
    [SerializeField] private Menu menu;

    private void Update()
    {
        blankMenu.Open = !PlayerInput.OpenTabMenu();
        menu.Open = PlayerInput.OpenTabMenu();
    }
}
