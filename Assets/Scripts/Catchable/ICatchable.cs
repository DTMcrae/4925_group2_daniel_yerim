using UnityEngine;

public abstract class ICatchable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnCatch();
            Destroy(gameObject, 0.01f);
        }
        else if(collision.tag == "Boundary")
        {
            OnMiss();
            Destroy(gameObject, 0.01f);
        }
    }

    public abstract void OnCatch();

    public abstract void OnMiss();
}
