using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortestPath : MonoBehaviour
{
    public PlayerInput playerInput;

    public bool Astar = false;
    private bool isExplored = false;

    public float weightInput = 1.0f;

    private GameObject[] nodes;

    public Text changeText;
    //public Text resultText;

    public Button changeAlgo;

    Renderer rend;

    private void Start()
    {
        changeAlgo.GetComponent<Image>().color = Color.yellow;
    }

    public List<Transform> FindShortestPath(Transform start, Transform end)
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");

        List<Transform> result = new List<Transform>();
        Transform node = Algorithm (start, end);

        // While there's still previous node, we will continue.
        while (node != null)
        {
            result.Add(node);
            Node currentNode = node.GetComponent<Node>();
            node = currentNode.GetParentNode();
        }

        // Reverse the list so that it will be from start to end.
        result.Reverse();
        return result;
    }
    
    private Transform Algorithm (Transform start, Transform end)
    {
        double startTime = Time.realtimeSinceStartup;

        // Nodes that are unexplored
        List<Transform> unexplored = new List<Transform>();

        // Add all the nodes found into unexplored.
        foreach (GameObject obj in nodes)
        {
            Node n = obj.GetComponent<Node>();
            if (n.IsWalkable())
            {
                n.ResetNode();
                unexplored.Add(obj.transform);
            }
        }

        // Set the starting node weight to 0;
        Node startNode = start.GetComponent<Node>();
        startNode.SetWeight(0);

        while (unexplored.Count > 0)
        {
            unexplored.Sort((x, y) => x.GetComponent<Node>().GetWeight().CompareTo(y.GetComponent<Node>().GetWeight()));
            
            Transform current = unexplored[0];
            
            unexplored.Remove(current);

            Node currentNode = current.GetComponent<Node>();
            List<Transform> neighbours = currentNode.getNeighbourNode();
            foreach (Transform neighNode in neighbours)
            {
                Node node = neighNode.GetComponent<Node>();
                rend = neighNode.GetComponent<Renderer>();
                Transform nodeTransform = node.GetComponent<Transform>();
                
                if (unexplored.Contains(neighNode) && node.IsWalkable())
                {
                    if (Astar == true)
                    {
                        float fCost = 0;
                        float gCost = Vector3.Distance(playerInput.endNode.position, neighNode.position);
                        float hCost = Vector3.Distance(playerInput.startNode.position, neighNode.position);
                        fCost = gCost + weightInput * hCost;

                        if (fCost < node.GetWeight() && !isExplored)
                        {
                            rend.material.color = Color.yellow;
                            node.SetWeight(fCost);
                            node.SetParentNode(current);
                        }
                        if (nodeTransform == playerInput.endNode)
                        {
                            isExplored = true;
                        }
                    }
                    else if (Astar == false)
                    {
                        float distance = Vector3.Distance(neighNode.position, current.position);
                        distance = currentNode.GetWeight() + distance;

                        if (distance < node.GetWeight() && !isExplored)
                        {
                            rend.material.color = Color.yellow;
                            node.SetWeight(distance);
                            node.SetParentNode(current);
                        }
                        if (nodeTransform == playerInput.endNode)
                        {
                            isExplored = true;
                        }
                    }
                }
            }
        }

        double endTime = (Time.realtimeSinceStartup - startTime);
        print("Compute time: " + endTime);

        print("Path completed!");

        return end;
    }

    public float Heuristic(Transform first, Transform second)
    {
        float distance = 0;
        float xDistance = Mathf.Abs(first.position.x - second.position.x);
        float zDistance = Mathf.Abs(first.position.z - second.position.z);
        distance = xDistance + zDistance;

        return distance;
    }

    public void SetAStar()
    {
        Astar = !Astar;
        if (Astar)
        {
            changeText.text = "AStar";
            changeAlgo.GetComponent<Image>().color = Color.cyan;
        }
        else if (!Astar)
        {
            changeText.text = "Dijkstra";
            changeAlgo.GetComponent<Image>().color = Color.yellow;
        }
    }

    public void Weight(InputField inputField)
    {
        float result = float.Parse(inputField.text);
        weightInput = result;
    }
}
