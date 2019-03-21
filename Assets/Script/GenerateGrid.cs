using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {

    int row = 25;
    int column = 25;
    public Transform nodePrefab;

    public List<Transform> grid = new List<Transform>();

    public bool isDiagonal = false;

    // Use this for initialization
    void Start () {
        Grid();
        GenerateNeighbours();
    }
	
    public void ToggleValue (bool newValue)
    {
        isDiagonal = newValue;
        GenerateNeighbours();
    }

	private void Grid()
    {
        int counter = 0;
        for(int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Transform node = Instantiate(nodePrefab, new Vector3((j * 1.5f) + gameObject.transform.position.x, gameObject.transform.position.y, (i * 1.5f) + gameObject.transform.position.z), Quaternion.identity);
                node.name = "node (" + counter + ")";
                grid.Add(node);
                counter++;
            }
        }
    }
    
    private void GenerateNeighbours()
    {
        for(int i = 0; i < grid.Count; i++)
        {
            Node currentNode = grid[i].GetComponent<Node>();

            int index = i + 1;
            
            // For those on the left, with no left neighbours
            if(index%column == 1)
            {
                // We want the node at the top as long as there is a node.
                if (i + column < column * row)
                {
                    currentNode.AddNeighbourNode(grid[i + column]);   // North node
                }

                if (i - column >= 0)
                {
                    currentNode.AddNeighbourNode(grid[i - column]);   // South node
                }
                currentNode.AddNeighbourNode(grid[i + 1]);     // East node

                if (isDiagonal)
                {
                    // We want the node at the top as long as there is a node.
                    if (i + column < column * row)
                    {
                        currentNode.AddNeighbourNode(grid[i + column]);   // North node
                        currentNode.AddNeighbourNode(grid[i + column + 1]);
                    }

                    if (i - column >= 0)
                    {
                        currentNode.AddNeighbourNode(grid[i - column]);   // South node
                        currentNode.AddNeighbourNode(grid[i - column + 1]);
                    }
                    currentNode.AddNeighbourNode(grid[i + 1]);     // East node
                }
            }
            
            // For those on the right, with no right neighbours
            else if (index%column == 0)
            {
                // We want the node at the top as long as there is a node.
                if (i + column < column * row)
                {
                    currentNode.AddNeighbourNode(grid[i + column]);   // North node
                }

                if (i - column >= 0)
                {
                    currentNode.AddNeighbourNode(grid[i - column]);   // South node
                }
                currentNode.AddNeighbourNode(grid[i - 1]);     // West node

                if (isDiagonal)
                {
                    // We want the node at the top as long as there is a node.
                    if (i + column < column * row)
                    {
                        currentNode.AddNeighbourNode(grid[i + column]);   // North node
                        currentNode.AddNeighbourNode(grid[i + column - 1]);
                    }

                    if (i - column >= 0)
                    {
                        currentNode.AddNeighbourNode(grid[i - column]);   // South node
                        currentNode.AddNeighbourNode(grid[i - column - 1]);
                    }
                    currentNode.AddNeighbourNode(grid[i - 1]);     // West node
                }
            }

            else
            {
                // We want the node at the top as long as there is a node.
                if (i + column < column * row)
                {
                    currentNode.AddNeighbourNode(grid[i + column]);   // North node
                }

                if (i - column >= 0)
                {
                    currentNode.AddNeighbourNode(grid[i - column]);   // South node
                }
                currentNode.AddNeighbourNode(grid[i + 1]);     // East node
                currentNode.AddNeighbourNode(grid[i - 1]);     // West node

                if (isDiagonal)
                {
                    // We want the node at the top as long as there is a node.
                    if (i + column + 1 < column * row)
                    {
                        currentNode.AddNeighbourNode(grid[i + column]);   // North node
                        currentNode.AddNeighbourNode(grid[i + column + 1]); // North East node
                        currentNode.AddNeighbourNode(grid[i + column - 1]); // North West node
                    }

                    if (i - column + 1 >= 0)
                    {
                        currentNode.AddNeighbourNode(grid[i - column]);   // South node
                        currentNode.AddNeighbourNode(grid[i - column + 1]); // South East node
                        currentNode.AddNeighbourNode(grid[i - column - 1]); // South West node
                    }
                    currentNode.AddNeighbourNode(grid[i + 1]);     // East node
                    currentNode.AddNeighbourNode(grid[i - 1]);     // West node
                }
            }

        }
    }
}
