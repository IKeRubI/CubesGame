using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static event EventHandler OnJump;

    public static Player Instance { get; private set; }



    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private GameInput gameInput;

    [SerializeField]
    private int jumpMaxCount = 2;
    private int jumpCount = 0;
    [SerializeField]
    private int jumpForce = 2;
    [SerializeField]
    private int jumpToFlourDistance = 2;
    [SerializeField]
    private LayerMask groundLayerMask;


    [SerializeField]
    private Rigidbody2D rig;

    [SerializeField]
    private Rifle rifle;
    


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More then one player Instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnJumpAction += GameInput_OnJumpAction;
    }


    private void Update()
    {
        HandleMovement();

        if (gameInput.GetShotButtonPressed())
        {
            rifle.Shoot();
        }
    }



    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectoNormalized();

        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        
        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }

    private void GameInput_OnJumpAction(object sender, EventArgs e)
    {

        Vector2 pos = new(transform.position.x, transform.position.y);
        
        if (Physics2D.Raycast(pos, Vector2.down, jumpToFlourDistance, groundLayerMask))
        {
            rig.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            jumpCount = jumpMaxCount;

            OnJump.Invoke(this, EventArgs.Empty);

        }
        else
        {
            if (jumpCount > 0)
            {
                rig.velocity = Vector2.zero;
                rig.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                jumpCount--;

                OnJump.Invoke(this, EventArgs.Empty);

            }
        }


    }

    private void OnDestroy()
    {
        gameInput.OnJumpAction -= GameInput_OnJumpAction;
    }
}
