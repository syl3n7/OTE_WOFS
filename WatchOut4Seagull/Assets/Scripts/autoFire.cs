using UnityEngine;

public class AutoFire : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public readonly float speed = 2;
    public Vector2 velocity;
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }
}
