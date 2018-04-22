using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingSea : MonoBehaviour {
    public float scrollingSpeed;
	
	// Update is called once per frame
	void Update () {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.y += Time.deltaTime * scrollingSpeed;

        mat.mainTextureOffset = offset;
	}
}
