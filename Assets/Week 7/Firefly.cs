using UnityEngine;

public class Firefly : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float speed = 1;
    [SerializeField] [Range(0, 20)] float interval = 10;
    private Vector3 startPos;
    private Vector3 target;
    private Vector3 velocity = Vector3.zero;
    private float lastTime;

    private void Start()
    {
        float offset = Random.value;
        foreach(var animator in GetComponentsInChildren<Animator>())
        {
            animator.SetFloat("Offset", offset);
        }
        lastTime = Time.time;
        startPos = transform.position;
        GetNewTarget(startPos, Random.value);
    }

    void Update()
    {
        if (Time.time - lastTime > interval)
        {
            GetNewTarget(startPos, Mathf.Max(startPos.y / 6, Random.value));
            lastTime = Time.time;
            target.y = startPos.y;
        }
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, speed);
    }

    private void GetNewTarget(Vector3 from, float dist)
    {
        target = Random.onUnitSphere * dist + from;
    }
}
