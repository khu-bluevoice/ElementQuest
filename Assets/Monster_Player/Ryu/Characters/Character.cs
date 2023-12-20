using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public InputManager inputManager;
    public PlayerInputAction playerAction;
    public float mouseSensitivity = 25f;
    private float yRotation = 0;


    public GameObject Earth1;
    // Start is called before the first frame update
    void Start()
    {
        playerAction = inputManager.playerInputAction;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = playerAction.CameraLook.MouseX.ReadValue<float>() * mouseSensitivity * Time.deltaTime;
        yRotation -= mouseX;
        //yRotation = Mathf.Clamp(yRotation, -180f, 180f);

        transform.rotation = Quaternion.Euler(0, -yRotation, 0);

    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Floor")
                {
                    gameObject.transform.position = hit.point;
                }
            }
        }
    }

    public void ShootEarth1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(Earth1, transform.position + transform.forward, transform.rotation);
        }
    }
}
