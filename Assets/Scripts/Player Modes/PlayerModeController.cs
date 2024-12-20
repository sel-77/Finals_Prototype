using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModeController : MonoBehaviour
{
    public enum PlayerMode { Interact, Shoot }
    public PlayerMode currentMode = PlayerMode.Interact;

    public bool canSwitchMode = true;

    public GameObject interactCrosshair;
    public GameObject shootCrosshair;
    public Transform crosshairTransform;

    public GameObject armWithGun; // Reference to the arm with gun object
    public Camera mainCamera; // Reference to the main camera

    private ShootMode shooter;
    private InteractMode interactor;

    void Start()
    {
        UpdateMode();
    }

    void Update()
    {
        FollowCursor();

        if (canSwitchMode && Input.GetKeyDown(KeyCode.Space))
        {
            ToggleMode();
        }
    }

    private void FollowCursor()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        crosshairTransform.position = new Vector3(mousePosition.x, mousePosition.y, crosshairTransform.position.z);
    }

    public void ToggleMode()
    {
        // Toggle between modes
        currentMode = currentMode == PlayerMode.Interact ? PlayerMode.Shoot : PlayerMode.Interact;
        UpdateMode();
    }

    private void UpdateMode()
    {
        if (currentMode == PlayerMode.Interact)
        {
            if (shooter != null) shooter.enabled = false;
            if (interactor != null) interactor.enabled = true;

            interactCrosshair.SetActive(true);
            shootCrosshair.SetActive(false);

            armWithGun.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (interactor != null) interactor.ResetCursorToDefault();
        }
        else if (currentMode == PlayerMode.Shoot)
        {
            if (shooter != null) shooter.enabled = true;
            if (interactor != null) interactor.enabled = false;

            interactCrosshair.SetActive(false);
            shootCrosshair.SetActive(true);

            armWithGun.SetActive(true);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}