using UnityEngine;

public class EchoShard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddShard(gameObject);
            gameObject.SetActive(false); 
        }
    }
}