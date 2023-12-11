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
    public float damage = 5;

    [Range(100, 500)]
    [SerializeField] private float speed;
    private Vector2 move;
    public float health = 100;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        movement();
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
            Debug.Log("EnemyHIT");
            TakeDamage(damage);
        }
    }
    private bool isTakingDamage = false;

    public void TakeDamage(float f)
    {
        if (!isTakingDamage)
        {
            StartCoroutine(DelayedDamage(f));
        }
    }

    private IEnumerator DelayedDamage(float f)
    {
        isTakingDamage = true;
        health -= f;
        if (health <= 0)
        {
            ReloadScene();
        }
        yield return new WaitForSeconds(2f);
        isTakingDamage = false;
    }

    private void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("In-Game");
    }

}