using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeSolver : MonoBehaviour {

    Stack<Vector3> points = new Stack<Vector3>();
    Graph graph;
    bool[] visited;
    GameObject AI;
    public Canvas canvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void solve()
    {

        Graph.Vertex start = null;
        graph = new Graph();
        for (int i=0;i<graph.vertices.Length;i++) //finds start node
        {
            //Debug.Log(graph.vertices[i]);
            if (graph.vertices[i].vertex.name.Equals("Start"))
            {
                start = graph.vertices[i];
            }
            if (start != null)
                break;
        }
        visited = new bool[graph.vertices.Length];
        if (dfs(start, visited, graph.vertices))
        {
            Debug.Log("Path found");
            canvas.GetComponent<Score>().showAITimeTrue();
            if(AI!=null) {
                Destroy(AI);
            }
            if (SceneManager.GetActiveScene().name.Equals("Competitive"))
            {
                StartCoroutine(wait());
                GameObject Player = Instantiate(Resources.Load("Player"), start.vertex.transform.position, Quaternion.identity) as GameObject;
            }
            AI = Instantiate(Resources.Load("AI"),start.vertex.transform.position,Quaternion.identity) as GameObject;
            AI.GetComponent<AIMovement>().receivePoints(points);
        } else {
            Debug.Log("No path");
            canvas.GetComponentInChildren<Fade>().appearFade();
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
    }
    public bool dfs(Graph.Vertex start, bool[] visited, Graph.Vertex[] vertices)
    {
        if (vertices[start.index].Equals("Finish")) {
            return true;
        }
        visited[start.index] = true;//INDEX HAS TO BE LOCATION IN VERTICES ARRAY
        for (int i=0;i<vertices[start.index].Neighbors.Count;i++)//goes through neighbors List
        {
            //Debug.Log("test index: " + i);
            //Debug.Log("visited[i]: " + visited[i]);    
            if (!visited[vertices[start.index].Neighbors[i].index])
            {
                
                if (vertices[start.index].Neighbors[i].vertex.name.Equals("Finish"))
                {
                    points.Push(vertices[start.index].Neighbors[i].vertex.transform.position);
                    return true;
                }
                if(dfs(vertices[vertices[start.index].Neighbors[i].index], visited, vertices)) {
                    points.Push(vertices[start.index].Neighbors[i].vertex.transform.position);
                    return true;
                }

            }
        }
        return false;
    }
    
}
