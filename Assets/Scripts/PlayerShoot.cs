using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{

    [Tooltip("The weapon used by the player")]
    public PlayerWeapon weapon;

    [SerializeField]
    [Tooltip("The camera used to shoot")]
    private GameObject cam;

    [SerializeField]
    [Tooltip("The layer on which the raycast will be casted")]
    private LayerMask mask;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("Aucune caméra associé au système de tir.");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (PlayerUI.isPaused)
            return;

        if (Input.GetButtonDown("Fire1")) // if we click on the left mouse button
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            Debug.Log("On a touché " + hit.collider.name);

            if (hit.collider.tag == "Player")
            {

            }
        }
    }
}
