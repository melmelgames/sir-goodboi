using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDone : MonoBehaviour
{
    public float timeToWait;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cleanup());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Cleanup()
    {
      yield return new WaitForSeconds(timeToWait);
      Destroy(gameObject);
    }
}
