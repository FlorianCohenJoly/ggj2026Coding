using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    public Transform cameraPivot;
    public PlayerInput playerInput;

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Look")]
    public float mouseSensitivity = 120f;
    public float minY = -40f;
    public float maxY = 60f;

    InputAction moveAction;
    InputAction lookAction;

    private Transform player;
    private Collider playerCollider;
    public Camera cam;
    public Material blindMaterial;
    public Material realMaterial;
    public InventoryManage inventory;

    float xRotation = 0f;
    private List<MeshRenderer> switchToTranspa = new List<MeshRenderer>();


    void Awake()
    {
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        player = transform;
        playerCollider = GetComponent<Collider>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        CameraObstruction();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Mask"))
        {
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

        Vector3 move =
            transform.forward * moveInput.y +
            transform.right * moveInput.x;

        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void HandleLook()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minY, maxY);

        cameraPivot.localRotation = Quaternion.Lerp(
            cameraPivot.localRotation,
            Quaternion.Euler(xRotation, 0, 0),
            0.1f // vitesse de lissage
        );
        transform.Rotate(Vector3.up * mouseX);
    }
}
