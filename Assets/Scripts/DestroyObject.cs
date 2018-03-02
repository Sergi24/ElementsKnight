using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public float destroyTime;

    private IEnumerator coroutineDestroy;

	// Use this for initialization
	void Start () {
        coroutineDestroy = Destroy(destroyTime);
        StartCoroutine(coroutineDestroy);
	}
	
	// Update is called once per frame
	IEnumerator Destroy (float destroyTime) {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
	}

    public void SetDestroyTime(float destroyTime)
    {
        StopCoroutine(coroutineDestroy);
        coroutineDestroy = Destroy(destroyTime);
        StartCoroutine(coroutineDestroy);
    }
}
