using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour {

    public int hitPoints = 2;
    public Sprite damagedSprite;
    public float damageImpactSpeed;

    private int currentHitPoints;
    private float damageImpactSpeedSqr;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D wingmanYellow;
    private Animator animator;

    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHitPoints = hitPoints;
        damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
        boxCollider2D = GetComponent<BoxCollider2D>();
        wingmanYellow = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag != "Damager")
        {
            return;
        }
        if(collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr)
        {
            return;
        }

        animator.enabled = false;
        spriteRenderer.sprite = damagedSprite;

        Debug.Log("Took Damage!");
        currentHitPoints--;

        if(currentHitPoints <= 0)
        {
            Kill();
        }

    }

    void Kill()
    {
        Debug.Log("Object Died!");
        
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        wingmanYellow.isKinematic = true;

        Destroy(gameObject);
        GameControlScript.score++;
        Debug.Log(GameControlScript.score);
    }

}
