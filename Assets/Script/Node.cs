using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [SerializeField] private float weight = int.MaxValue;
    [SerializeField] private Transform parentNode = null;
    [SerializeField] private List<Transform> neighbourNode;
    [SerializeField] private bool walkable = true;

    // Use this for initialization
    void Start () {
        ResetNode();
    }
    
    public void ResetNode()
    {
        weight = int.MaxValue;
        parentNode = null;
    }

    // Setters
    
    public void SetParentNode(Transform node)
    {
        parentNode = node;
    }
    
    public void SetWeight(float value)
    {
        weight = value;
    }
    
    public void SetWalkable(bool value)
    {
        walkable = value;
    }
    
    public void AddNeighbourNode(Transform node)
    {
        neighbourNode.Add(node);
    }

    // Getters
    
    public List<Transform> getNeighbourNode()
    {
        List<Transform> result = neighbourNode;
        return result;
    }

    public float GetWeight()
    {
        float result = weight;
        return result;

    }
    
    public Transform GetParentNode()
    {
        Transform result = parentNode;
        return result;
    }

    // Checkers
    
    public bool IsWalkable()
    {
        bool result = walkable;
        return result;
    }
}
