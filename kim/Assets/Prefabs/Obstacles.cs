using UnityEngine;

public class Obstacles : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.name.Equals("Wave"))
        {
            Destroy(gameObject);
            Debug.Log("Destroying "+gameObject);

        }
        else
        {
            Debug.Log("Collided with "+collisionInfo.gameObject.name);
        }
        
    }

}
