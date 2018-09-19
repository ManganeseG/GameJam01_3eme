using UnityEngine;

public class Patrol : MonoBehaviour
{
    public PathGenerator Path = null;

    public float Speed = 0.5f;
    public float TurnSpeed = 180.0f;

    private Vector3 m_lastLoc;

    private float m_progress = 0.0f;
    private float speedRotation = 5.0f;

    void Start()
    {
        m_lastLoc = transform.position;
    }

    void Update()
    {
        m_progress += ((1.0f + Path.GetTotalDistance()) * Speed / 100) * Time.deltaTime;

        Vector3 pos = new Vector3();
        Path.GetPointOnPath(m_progress, ref pos);
        transform.position = pos;

        if (m_lastLoc != transform.position)
        {
            Vector3 dir = transform.position - m_lastLoc;
            dir.Normalize();

            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, TurnSpeed * Time.deltaTime);
        }
        m_lastLoc = transform.position;
    }
}
