using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public float speed;

    private bool destroy;

    private void Awake()
    {
        destroy = false;
    }

    private void Start()
    {
        StartCoroutine(DestroyAmmo());
    }

    private void Update()
    {
        if (destroy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    IEnumerator DestroyAmmo()
    {
        for (int counter = 0; counter <= 30; counter++)
        {
            if (!destroy)
            {
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                yield break;
            }
        }

        destroy = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            destroy = true;
        }
    }
}
