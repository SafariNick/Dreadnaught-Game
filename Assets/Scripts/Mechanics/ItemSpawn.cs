using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] gameObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int RandNum = Random.Range(0, gameObjects.Length);

        Instantiate(gameObjects[RandNum], transform.position, Quaternion.identity);
    }
}