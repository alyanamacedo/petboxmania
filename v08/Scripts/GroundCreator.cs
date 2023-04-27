using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCreator : MonoBehaviour
{
    public List<GameObject> childObjects;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            childObjects.Add(child.gameObject);
        }
        foreach (GameObject child in childObjects){  
            Vector3 targetPosition = child.transform.position;
            // Randomize the cube's starting position
            Vector3 startPosition = Vector3.zero;
            int randomValue = Random.Range(1, 4);
            switch (randomValue)
            {
                case 1:
                    startPosition = new Vector3(targetPosition.x, targetPosition.y, Random.Range(-100f, 100f));
                    break;
                case 2:
                    startPosition = new Vector3(targetPosition.x, Random.Range(-100f, 100f), targetPosition.z);
                    break;
                case 3:
                    startPosition = new Vector3(Random.Range(-100f, 100f), targetPosition.y, targetPosition.z);
                    break;
            }
           
            child.transform.position = startPosition;
            // Apply a coroutine to move the cube towards the target position over time
            StartCoroutine(MoveToPosition(child, startPosition, targetPosition, Random.Range(0f, 2f)));
        }
    }

    IEnumerator MoveToPosition(GameObject cube, Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // Calculate the percentage of the movement completed
            // Use a smooth step function to move the cube towards the target position
            cube.transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null; // Wait for the next frame
        }

        cube.transform.position = targetPosition; // Ensure the cube reaches the exact target position
    }
}