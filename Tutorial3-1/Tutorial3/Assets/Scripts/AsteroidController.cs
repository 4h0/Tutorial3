using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject[] explosion;
    public GameObject[] asteroidMesh;

    private GameController gameControllerReference;

    public float tumble;
    public float speed;

    private bool destroy;
    private float whichMesh;

    private void Awake()
    {
        gameControllerReference = FindObjectOfType<GameController>();

        for (int counter = 0; counter < asteroidMesh.Length; counter++)
        {
            asteroidMesh[counter].SetActive(false);
        }

        whichMesh = Random.Range(-1f, 1f);

        if (whichMesh < 0)
        {
            asteroidMesh[0].SetActive(true);
        }
        if (whichMesh == 0)
        {
            asteroidMesh[1].SetActive(true);
        }
        if (whichMesh > 0)
        {
            asteroidMesh[2].SetActive(true);
        }

        destroy = false;
    }

    private void Start()
    {
        StartCoroutine(DestroyAsteroid());
    }

    private void Update()
    {
        if (destroy)
        {
            Instantiate(explosion[0], this.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }
    }

    IEnumerator DestroyAsteroid()
    {
        for (int counter = 0; counter <= 60; counter++)
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
        if (collision.gameObject.tag == "Bullet")
        {
            destroy = true;

            gameControllerReference.score++;
        }

        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion[1], this.transform.position, Quaternion.identity);

            gameControllerReference.gameEnd = true;
            gameControllerReference.score++;

            Destroy(collision.gameObject);
        }
    }
}
