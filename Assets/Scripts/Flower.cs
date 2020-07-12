using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {
	public Sprite[] flowerSprites;
	public LayerMask collisionMask;
	SpriteRenderer spriteRenderer;
	// Start is called before the first frame update
	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = flowerSprites[Random.Range(0, flowerSprites.Length - 1)];
	}

	void OnTriggerEnter2D(Collider2D col) {
		//This should use the collisionMask...but I'm not sure how right nowo and this is easier
		if (col.gameObject.layer == 10) {
			print("Flower Destroyed!");
			Destroy(gameObject);
		}
	}
}
