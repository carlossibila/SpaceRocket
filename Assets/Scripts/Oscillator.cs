using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 moveVector;
    [SerializeField] float speed;
    Vector3 startPosition;
    Vector3 endPosition;
    float moveFactor;



    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + moveVector;
    }

    void Update()
    {
        moveFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, moveFactor);
    }
}
