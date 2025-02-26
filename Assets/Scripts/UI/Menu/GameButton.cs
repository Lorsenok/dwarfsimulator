using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ButtonFunction
{
    Nothing,
    Start,
    Exit,
    DeleteAllData,
    MenuOpen,
    Restart,
    DeleteData,
    ClearSettings
}

public class GameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool CanBeToched { get; set; } = true;

    [Header("Tech")]
    [SerializeField] protected int mouseButton;
    [SerializeField] protected ButtonFunction OnClick;
    [SerializeField] protected string index;

    [Header("Animations")]
    [SerializeField] protected Animator animator;

    [SerializeField] protected string IsClicking;
    [SerializeField] protected string IsPointing;
    [SerializeField] protected string IsNotPointing;

    [SerializeField] protected Vector3 moveDirection;
    [SerializeField] protected float moveSpeed;

    [Header("Sounds")]
    [SerializeField] private AudioSource overSound;
    private bool onOverSound = true;
    [SerializeField] private AudioSource clickSound;
    private bool onClickSound = true;

    protected bool isMousePointing = false;

    protected Vector3 startPos;

    public virtual void OnMouseOver()
    {
        isMousePointing = true;

        if (onOverSound && CanBeToched)
        {
            if (overSound != null) overSound.Play();
            onOverSound = false;
        }
    }

    public virtual void OnMouseExit()
    {
        isMousePointing = false;

        onOverSound = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseOver();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit();
    }

    public virtual void Awake()
    {
        CanBeToched = true;
        startPos = transform.localPosition;
    }

    public virtual void Update()
    {
        if (overSound != null) overSound.volume = Config.Sound;
        if (clickSound != null) clickSound.volume = Config.Sound;

        if (!CanBeToched)
        {
            isMousePointing = false;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, isMousePointing ? startPos + moveDirection / (Input.GetMouseButton(mouseButton) ? 2 : 1) : startPos, Time.deltaTime * moveSpeed);

        animator.SetBool(IsPointing, isMousePointing && !Input.GetMouseButton(mouseButton));
        animator.SetBool(IsClicking, isMousePointing && Input.GetMouseButton(mouseButton));
        animator.SetBool(IsNotPointing, !isMousePointing);

        if (isMousePointing && Input.GetMouseButtonDown(mouseButton))
        {
            onClickSound = false;
        }

        if (isMousePointing && Input.GetMouseButtonUp(mouseButton))
        {
            switch (OnClick)
            {
                case ButtonFunction.Start:
                    SceneSwitcher.Instance.ChangeScene(index);
                    break;

                case ButtonFunction.Exit:
                    Application.Quit();
                    break;

                case ButtonFunction.DeleteAllData:
                    PlayerPrefs.DeleteAll();
                    SceneSwitcher.Instance.ChangeScene(index);
                    CanBeToched = false;
                    break;

                case ButtonFunction.MenuOpen:
                    MenuManager.Instance.MenuOpen(index);
                    break;

                case ButtonFunction.Restart:
                    SceneSwitcher.Instance.ChangeScene(SceneManager.GetActiveScene().name);
                    break;

                case ButtonFunction.DeleteData:
                    PlayerPrefs.DeleteAll();
                    SceneSwitcher.Instance.ChangeScene(SceneManager.GetActiveScene().name);
                    break;

                case ButtonFunction.ClearSettings:
                    SettingsSetup.Instance.ClearSettings();
                    break;
            }
        }


        if (!onClickSound)
        {
            if (clickSound != null) clickSound.Play();
            onClickSound = true;
        }
    }
}
