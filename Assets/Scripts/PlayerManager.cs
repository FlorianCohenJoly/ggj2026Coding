using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    public PlayerInput playerInput;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public UIManager uIManager;
    public CharacterController controller;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Look")]
    public float mouseSensitivity = 120f;
    public float minY = -40f;
    public float maxY = 60f;

    InputAction moveAction;
    InputAction menuAction;
    InputAction scrollAction;
    InputAction interactAction;

    private Transform player;
    public Camera cam;
    public Material blindMaterial;
    public Material realMaterial;  // a revoir 
    public InventoryManage inventory;
    public Transform model;

    float xRotation = 0f;
    private List<MeshRenderer> switchToTranspa = new List<MeshRenderer>();
    private float coyoteTime = 0.2f;     
    private float coyoteTimeCounter;

    public Animator animator;
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip footstep;
    public AudioClip lootSound;

    private bool wasGrounded;


    void Awake()
    {
        moveAction = playerInput.actions["Move"];
        menuAction = playerInput.actions["Menu"];
        scrollAction = playerInput.actions["Scroll"];
        interactAction = playerInput.actions["Jump"];
        player = transform;
    }

    void Start()
    {
       /* Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
    }

    void Update()
    {
        ItemChoice();
        HandleMovement();
        ApplyGravity();
        CameraObstruction();
        InteractItem();
      /*  if (menuAction.triggered)
        {
            uIManager.OpenMenu();
        }*/
    }

    void InteractItem()
    {
        if (interactAction.triggered && inventory.currentSelected != null)
        {
            inventory.currentSelected.UseMask();
        }
    }

    void ItemChoice()
    {
        Vector2 scroll = scrollAction.ReadValue<Vector2>();
        if (scroll.y > 0.1f)
            inventory.NextSlot();
        else if (scroll.y < -0.1f)
            inventory.PreviousSlot();
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Mask"))
        {
            if(audioSource && lootSound)
                audioSource.PlayOneShot(lootSound);
            collision.gameObject.SetActive(false);
            inventory.AddMask();
        }
    }

    void CameraObstruction()
    {
        // Rayon caméra → joueur
        Vector3 dir = player.position - cam.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        // Debug visuel
        Debug.DrawLine(cam.transform.position, player.position, Color.red);

        // Nouveaux murs détectés cette frame
        List<MeshRenderer> currentFrameHits = new List<MeshRenderer>();

        // RaycastAll pour tous les objets entre caméra et joueur
        RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, dir, dist);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform == player) continue;

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("MaskableWall"))
            {
                MeshRenderer wallMat = hit.transform.GetComponent<MeshRenderer>();
                if (wallMat != null)
                {
                    // On applique le matériau transparent
                    wallMat.material = blindMaterial;

                    // On ajoute à la liste de murs détectés cette frame
                    currentFrameHits.Add(wallMat);

                    // Si pas déjà dans switchToTranspa, on l'ajoute
                    if (!switchToTranspa.Contains(wallMat))
                        switchToTranspa.Add(wallMat);
                }
            }
        }

        // Parcours de la liste existante pour rétablir les murs qui ne sont plus touchés
        for (int i = switchToTranspa.Count - 1; i >= 0; i--)
        {
            MeshRenderer wall = switchToTranspa[i];
            if (!currentFrameHits.Contains(wall))
            {
                // Rétablir matériau normal
                wall.material = realMaterial;

                // Retirer de la liste
                switchToTranspa.RemoveAt(i);
            }
        }
    }

    void HandleMovement()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // Direction caméra (à plat)
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();
    
        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;
        animator.SetBool("IsMoving", move.magnitude > 0.1f);
        if(move.magnitude > 0.1f && controller.isGrounded)
        {
            if(!audioSource.isPlaying)
                audioSource.PlayOneShot(footstep);
        }
        controller.Move(move * moveSpeed * Time.deltaTime);

        ApplyGravity();

        RotateModel(move);
    }

    void RotateModel(Vector3 move)
    {
        if (move.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRot = Quaternion.LookRotation(move);
        model.rotation = Quaternion.Slerp(
            model.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    public void Jump(float jumpForce)
    {
        if (controller.isGrounded)
        {
            animator.SetTrigger("Jump");
            velocity.y = jumpForce;
           //coyoteTimeCounter = 0f;
           if(audioSource && jumpSound)
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void ApplyGravity()
    {

        animator.SetBool("IsGrounded", controller.isGrounded);
        if (controller.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f; // 👈 petite force vers le bas pour rester collé au sol
            if(!wasGrounded) // si avant on était en l'air et maintenant au sol
            {
                if(audioSource && landSound)
                    audioSource.PlayOneShot(landSound);
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
        wasGrounded = controller.isGrounded;
    }
}
