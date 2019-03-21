using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour {

    private Transform node;
    public Transform startNode;
    public Transform endNode;
    private List<Transform> blockPath = new List<Transform>();

	// Update is called once per frame
	void Update () {
        MouseInput();
    }
    
    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Update colors for every mouse clicked.
            ColorBlockPath();
            UpdateNodeColor();

            // Get the raycast from the mouse position from screen.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Node")
            {
                //unmark previous
                Renderer rend;
                if (node != null)
                {
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.white;
                }

                // We now update the selected node.
                node = hit.transform;

                // Mark it
                rend = node.GetComponent<Renderer>();
                rend.material.color = Color.green;

            }
        }
    }
    
    public void BtnStartNode()
    {
        if (node != null)
        {
            Node n = node.GetComponent<Node>();

            // Making sure only walkable node are able to set as start.
            if (n.IsWalkable())
            {
                // If this is a new start node, we will just set it to blue.
                if (startNode == null)
                {
                    Renderer rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.blue;
                }
                else
                {
                    // Reverse the color of the previous node
                    Renderer rend = startNode.GetComponent<Renderer>();
                    rend.material.color = Color.white;

                    // Set the new node as blue.
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.blue;
                }

                startNode = node;
                node = null;
            }
        }
    }
    
    public void BtnEndNode()
    {
        if (node != null)
        {
            Node n = node.GetComponent<Node>();

            // Making sure only walkable node are able to set as end.
            if (n.IsWalkable())
            {
                // If this is a new end node, we will just set it to cyan.
                if (endNode == null)
                {
                    Renderer rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.cyan;
                }
                else
                {
                    // Reverse the color of the previous node
                    Renderer rend = endNode.GetComponent<Renderer>();
                    rend.material.color = Color.white;

                    // Set the new node as cyan.
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.cyan;
                }

                endNode = node;
                node = null;
            }
        }
    }
    
    public void BtnFindPath()
    {
        // Only find if there are start and end node.
        if (startNode != null && endNode != null)
        {
            // Execute Shortest Path.
            ShortestPath finder = gameObject.GetComponent<ShortestPath>();
            List<Transform> paths = finder.FindShortestPath(startNode, endNode);

            // Colour the node red.
            foreach (Transform path in paths)
            {
                Renderer rend = path.GetComponent<Renderer>();
                rend.material.color = Color.red;
            }
        }
    }
    
    public void BtnBlockPath()
    {
        if (node != null)
        {
            // Render the selected node to black.
            Renderer rend = node.GetComponent<Renderer>();
            rend.material.color = Color.black;

            // Set selected node to not walkable
            Node n = node.GetComponent<Node>();
            n.SetWalkable(false);

            // Add the node to the block path list.
            blockPath.Add(node);

            // If the block path is start node, we remove start node.
            if (node == startNode)
            {
                startNode = null;
            }

            // If the block path is end node, we remove end node.
            if (node == endNode)
            {
                endNode = null;
            }

            node = null;
        }

        // For selection grid system.
        UnitSelectionComponent selection = gameObject.GetComponent<UnitSelectionComponent>();
        List<Transform> selected = selection.getSelectedObjects();

        foreach(Transform nd in selected)
        {
            // Render the selected node to black.
            Renderer rend = nd.GetComponent<Renderer>();
            rend.material.color = Color.black;

            // Set selected node to not walkable
            Node n = nd.GetComponent<Node>();
            n.SetWalkable(false);

            // Add the node to the block path list.
            blockPath.Add(nd);

            // If the block path is start node, we remove start node.
            if (nd == startNode)
            {
                startNode = null;
            }

            // If the block path is end node, we remove end node.
            if (nd == endNode)
            {
                endNode = null;
            }
        }

        selection.clearSelections();
    }
    
    public void BtnUnblockPath()
    {
        if (node != null)
        {
            // Set selected node to white.
            Renderer rend = node.GetComponent<Renderer>();
            rend.material.color = Color.white;

            // Set selected not to walkable.
            Node n = node.GetComponent<Node>();
            n.SetWalkable(true);

            // Remove selected node from the block path list.
            blockPath.Remove(node);

            node = null;
        }

        // For selection grid system.
        UnitSelectionComponent selection = gameObject.GetComponent<UnitSelectionComponent>();
        List<Transform> selected = selection.getSelectedObjects();

        foreach (Transform nd in selected)
        {
            // Set selected node to white.
            Renderer rend = nd.GetComponent<Renderer>();
            rend.material.color = Color.white;

            // Set selected not to walkable.
            Node n = nd.GetComponent<Node>();
            n.SetWalkable(true);

            // Remove selected node from the block path list.
            blockPath.Remove(nd);
        }

        selection.clearSelections();
    }
    
    public void BtnClearBlock()
    {   
        // For each blocked path in the list
        foreach(Transform path in blockPath)
        {   
            // Set walkable to true.
            Node n = path.GetComponent<Node>();
            n.SetWalkable(true);

            // Set their color to white.
            Renderer rend = path.GetComponent<Renderer>();
            rend.material.color = Color.white;

        }
        // Clear the block path list and 
        blockPath.Clear();
    }
    
    public void BtnRestart()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    private void ColorBlockPath()
    {
        foreach(Transform block in blockPath)
        {
            Renderer rend = block.GetComponent<Renderer>();
            rend.material.color = Color.black;
        }
    }
    
    private void UpdateNodeColor()
    {
        if (startNode != null)
        {
            Renderer rend = startNode.GetComponent<Renderer>();
            rend.material.color = Color.blue;
        }

        if (endNode != null)
        {
            Renderer rend = endNode.GetComponent<Renderer>();
            rend.material.color = Color.cyan;
        }
    }
}
