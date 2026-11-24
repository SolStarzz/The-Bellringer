using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(0f, 4f, 0f);
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 endPos;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + moveOffset;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed / Vector3.Distance(startPos, endPos), 1f);
        transform.position = Vector3.Lerp(startPos, endPos, t);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}