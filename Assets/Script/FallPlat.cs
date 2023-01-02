using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
	public float fallTime = 0.5f;
    public float pushPower = 2.0f;

    
    //void OnCollisionEnter(Collision collision)
    //{
    //    foreach (ContactPoint contact in collision.contacts)
    //    {
    //        //Debug.DrawRay(contact.point, contact.normal, Color.white);
    //        if (collision.gameObject.tag == "Player")
    //        {
    //            StartCoroutine(Fall(fallTime));
    //            test = true;
    //        }
    //    }
    //}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (hit.collider.CompareTag("Player"))
        {
            StartCoroutine(Fall(fallTime));
            Debug.Log("hit");
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }

    IEnumerator Fall(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
    public void destroyThis()
    {
        Invoke("destroyGlass",0.5f);
    }
    public void destroyGlass()
    {
        Destroy(this.gameObject);
    }
}
