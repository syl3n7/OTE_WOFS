using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float coolDown = 3f;
    public float damage, lenght;
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public Transform firePoint;

    [Range(1, 25)]
    public float bulletForce = 10f;

    [Range(1, 15)]
    [SerializeField] private float speed;
    private Vector2 move, mouseLook, joystickLook;
    private Vector3 rotationTarget;
    public bool pc; //boolean to later chose between controller and mouse on settings menu

    private enum state { idle, walking, running, shooting, dead }; // for later use on animations

    public float health = 50;
    public bool canMelee;
    public bool canShoot1;
    public bool canShoot2;

    [SerializeField] private VisualEffect n1, p1, p2, p3;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
    }
// fbCivFYT
    public void OnFire(InputAction.CallbackContext context)
    { // call Shoot method when X or LMB is pressed
        context.action.performed += ctx =>
        {
            if (canMelee && n1 != null)
            {
                Melee();
                n1.Play();
            }
        };
    }

    public void OnOne(InputAction.CallbackContext context)
    { // call Shoot method when X or LMB is pressed
        context.action.performed += ctx =>
        {
            if (keystone1 && canShoot1) Keystone1();
        };
    }

    public void OnTwo(InputAction.CallbackContext context)
    { // call Shoot method when X or LMB is pressed
        context.action.performed += ctx =>
        {
            if (keystone2 && canShoot2) Keystone2();
        };
    }

    public void OnThree(InputAction.CallbackContext context)
    { // call Shoot method when X or LMB is pressed
        context.action.performed += ctx =>
        {
            if (keystone3 && canShoot3) Keystone3();
        };
    }

    void Start()
    {
        pc = true;
        canMelee = true;
        canShoot1 = true;
        canShoot2 = true;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * lenght, Color.green);
        if (pc)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(mouseLook);

            if (Physics.Raycast(ray, out hit))
            {
                rotationTarget = hit.point;
            }
            mouseMovement();
        }
        else
        {
            if (joystickLook.x == 0 && joystickLook.y == 0)
            {
                movement();
            }
            else
            {
                mouseMovement();
            }
        }
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.3f);
        canMelee = true;
    }

    IEnumerator cooldown1()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot1 = true;
    }

    IEnumerator cooldown2()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot2 = true;
    }

    public void movement()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        /*if statement to fix the player looking up after release of the controls*/
        if (movement != Vector3.zero) transform.Translate(movement * speed * Time.deltaTime, Space.World);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
    }

    public void mouseMovement()
    {
        if (pc)
        {
            var lookPos = rotationTarget - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(joystickLook.x, 0f, joystickLook.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.15f);
            }
        }
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * (speed * Time.deltaTime), Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            ReloadScene();
        }
    }

    public void TakeDamage(float f)
    {
        health -= f;
        if (health <= 0)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("01");
    }

    public void Keystone1()
    {
        GameObject bullet1 = Instantiate(bulletPrefab1, firePoint.position, gameObject.transform.rotation);
        Rigidbody rb = bullet1.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        canShoot1 = false;
        StartCoroutine("cooldown1");
        //animator.Play("Attack02");
    }

    public void Keystone2()
    {
        GameObject bullet2 = Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet2.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        canShoot2 = false;
        StartCoroutine("cooldown2");
        if (n1 != null) p2.Play();
    }


}