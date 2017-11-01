using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour {

    public float maxStretch = 2.0f;

    private bool clickedOn;

    public LineRenderer catapultLineFront;
    public LineRenderer catapultLineBack;

    private SpringJoint2D spring;
    private Transform catapult;
    private Rigidbody2D pigHead;
    private Ray rayToMouse;
    private Ray frontCatapultToProjectile;
    private float maxStretchSqr;
    private Vector2 prevVelocity;
    private float circleRadius;


    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        pigHead = GetComponent<Rigidbody2D>();
        catapult = spring.connectedBody.transform;
    }


    // Use this for initialization
    void Start () {

        LineRendererSetup();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        frontCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
        maxStretchSqr = maxStretch * maxStretch;
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        circleRadius = circle.radius;

    }
	
	// Update is called once per frame
	void Update () {

        if(clickedOn)
        {
            Dragging();
        }

		if(spring != null)
        {
            if(!pigHead.isKinematic && prevVelocity.sqrMagnitude > pigHead.velocity.sqrMagnitude)
            {
                Destroy(spring);
                pigHead.velocity = prevVelocity;
            }

            if(!clickedOn)
            {
                prevVelocity = pigHead.velocity;
            }

            LineRendererUpdate();
        }
        else
        {
            catapultLineFront.enabled = false;
            catapultLineBack.enabled = false;
        }
	}

    void LineRendererSetup()
    {
        catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
        catapultLineBack.SetPosition(0, catapultLineBack.transform.position);

        catapultLineFront.sortingLayerName = "Ground";
        catapultLineBack.sortingLayerName = "Ground";

        catapultLineFront.sortingOrder = 9;
        catapultLineBack.sortingOrder = 7;
    }

    void LineRendererUpdate()
    {
        Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
        frontCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = frontCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + .25f);
        catapultLineFront.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);
    }

    void OnMouseDown()
    {
        spring.enabled = false;
        clickedOn = true;
    }

    void OnMouseUp()
    {
        spring.enabled = true;
        pigHead.isKinematic = false;
        pigHead.angularDrag = 5.0f;
        pigHead.drag = 0.1f;
        clickedOn = false;
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

        if(catapultToMouse.sqrMagnitude > maxStretchSqr)
        {
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }

        mouseWorldPoint.z = 0.0f;
        transform.position = mouseWorldPoint;
    }

}
