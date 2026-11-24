using UnityEngine;

public class ShadowWisp : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(4f, 0f, 0f);
    public float speed = 2.5f;

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
}