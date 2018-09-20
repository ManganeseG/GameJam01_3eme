using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PathNode
{
    public Transform transform;
    public float dist;
    public float minDist;
    public float maxDist;
}

public class PathGenerator : MonoBehaviour
{
    public bool looped;
    List<PathNode> m_nodes = new List<PathNode>();
    float m_totalDist;

    void Start()
    {
        FindNodes(transform);
        CalculatePath();
    }

    void FindNodes(Transform parent)
    {
        foreach(Transform child in parent)
        {
            if(child.childCount == 0)
            {
                PathNode newNode = new PathNode();
                newNode.transform = child;
                m_nodes.Add(newNode);
            }

            FindNodes(child);
        }
    }

    void CalculatePath()
    {
        float totalDist = 0.0f;

        for (int i = 0; i < m_nodes.Count; i++)
        {
            PathNode curNode = m_nodes[i];
            PathNode nextNode;

            if (looped && i + 1 == m_nodes.Count)
                nextNode = m_nodes[0];
            else if (i + 1 != m_nodes.Count)
                nextNode = m_nodes[i + 1];
            else
                nextNode = new PathNode();

            float dist = 0.0f;

            if (nextNode.transform)
                dist = Vector3.Distance(curNode.transform.position, nextNode.transform.position);

            curNode.dist = dist;
            curNode.minDist = totalDist;
            totalDist += dist;
            curNode.maxDist = totalDist;

            m_nodes[i] = curNode;
        }
        m_totalDist = totalDist;
    }

    public PathNode GetPointOnPath(float progress, ref Vector3 pos)
    {
        if (looped)
            progress = progress - Mathf.Floor(progress);
        else if (progress > 1.0f)
            progress = 1.0f;
        else if (progress < 0.0f)
            progress = 0.0f;

        float curDist = m_totalDist * progress;
        int id = (int)Mathf.Floor(m_nodes.Count / 2.0f);

        while(true)
        {
            if(m_nodes[id].minDist > curDist)
            {
                id--;
            }
            else if (m_nodes[id].maxDist < curDist)
            {
                id++;
            }
            else if (m_nodes[id].minDist <= curDist && m_nodes[id].maxDist >= curDist)
            {
                float lerpProg = m_nodes[id].dist == 0.0f ? 0.0f : (curDist - m_nodes[id].minDist) / m_nodes[id].dist;
                pos = Vector3.Lerp(m_nodes[id].transform.position, m_nodes[id + 1 >= m_nodes.Count ? 0 : id + 1].transform.position, lerpProg);
                return m_nodes[id];
            }
        }
    }

    public float GetTotalDistance()
    {
        return m_totalDist;
    }
}
