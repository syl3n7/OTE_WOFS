using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float damage, lenght;
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public Transform firePoint;
    public bool canShoot = true;

    [Range(1, 25)]
    public float bulletForce = 100f;

    [Range(100, 500)]
    [SerializeField] private float speed;
    private Vector2 move;
    public float health = 100;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    //? fbCivFYT | nao sei o que é isto - claudio
    public void OnFire(InputAction.CallbackContext context)
    { // call Shoot method when X or LMB is pressed
        context.action.performed += ctx =>
        {
            //!add bullet firing code alternated with fish1 and fish2
            int rnd = UnityEngine.Random.Range(1, 10);
            GameObject bullet = Instantiate(bulletPrefab1, firePoint.position, firePoint.rotation);
            bullet.transform.SetParent(GameObject.Find("firing_point").transform);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            canShoot = false;
            StartCoroutine("cooldown");
        };
    }

    private void Update()
    {
        movement();
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
    }

    public void movement()
    {
        Vector3 movement = new Vector3(move.x, move.y, 0f);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("In-Game");
    }

}