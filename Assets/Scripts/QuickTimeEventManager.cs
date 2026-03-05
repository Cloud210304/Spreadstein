using System.Collections;
using UnityEngine;

public class RandomQuickTimeEventManager : MonoBehaviour
{
    [Header("QTE Settings")]
    [Tooltip("List of possible QTE GameObjects")]
    public GameObject[] quickTimeEventObjects;

    [Tooltip("Minimum time before QTE appears")]
    public float minTime = 45f;

    [Tooltip("Maximum time before QTE appears")]
    public float maxTime = 120f;

    private GameObject activeQTE;

    void Start()
    {
        // Make sure all QTE objects are hidden at start
        foreach (GameObject qte in quickTimeEventObjects)
        {
            if (qte != null)
                qte.SetActive(false);
        }

        StartCoroutine(QTERoutine());
    }

    private IEnumerator QTERoutine()
    {
        while (true)
        {
            // Wait a random time before next QTE
            float waitTime = Random.Range(minTime, maxTime);
            Debug.Log("Next QTE in: " + waitTime + " seconds");
            yield return new WaitForSeconds(waitTime);

            // Pick a random QTE object
            if (quickTimeEventObjects.Length == 0)
                yield break; // no QTE objects assigned

            int randomIndex = Random.Range(0, quickTimeEventObjects.Length);
            activeQTE = quickTimeEventObjects[randomIndex];

            if (activeQTE != null)
            {
                activeQTE.SetActive(true);
                Debug.Log("QTE started: " + activeQTE.name);

                // Wait until QTE is closed (set inactive)
                yield return new WaitUntil(() => activeQTE.activeSelf == false);

                Debug.Log("QTE closed: " + activeQTE.name);
            }
        }
    }

    public void CloseActiveQTE()
    {
        activeQTE.SetActive(false);
    }
}
