using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {

    public Vertex[] vertices;
    private GameObject[] nodes;

    public Graph()
    {
        getNodes();
        getNeighbors();
        //printGraph();
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void getNodes()
    {
        this.nodes = GameObject.FindGameObjectsWithTag("node");
        this.vertices = new Vertex[nodes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            vertices[i] = new Vertex(i,nodes[i]);
            /*vertices[i].vertex = nodes[i];
            vertices[i].index = i;*/
            //Debug.Log(vertices[i]);
        }
    }

    public void getNeighbors()
    {

        for (int i = 0; i < vertices.Length; i++)
        {
            RaycastHit hit1,hit2,hit3,hit4;
            if (Physics.Raycast(vertices[i].vertex.transform.position, Vector3.forward, out hit1, 1.5f))
            {
                if (hit1.transform.gameObject.tag.Equals("node"))
                {
                    vertices[i].Neighbors.Add(new Neighbor(getVertexIndex(hit1.transform.gameObject),hit1.transform.gameObject));
                }
            }
            if (Physics.Raycast(vertices[i].vertex.transform.position, Vector3.back, out hit2, 1.5f))
            {
                if (hit2.transform.gameObject.tag.Equals("node"))
                {
                    vertices[i].Neighbors.Add(new Neighbor(getVertexIndex(hit2.transform.gameObject),hit2.transform.gameObject));
                }
            }
            if (Physics.Raycast(vertices[i].vertex.transform.position, Vector3.left, out hit3, 1.5f))
            {
                if (hit3.transform.gameObject.tag.Equals("node"))
                {
                    vertices[i].Neighbors.Add(new Neighbor(getVertexIndex(hit3.transform.gameObject),hit3.transform.gameObject));
                }
            }
            Debug.DrawRay(vertices[i].vertex.transform.position,Vector3.right, Color.red, 10);
            if (Physics.Raycast(vertices[i].vertex.transform.position, Vector3.right, out hit4, 1.5f))
            {
                if (hit4.transform.gameObject.tag.Equals("node"))
                {
                    vertices[i].Neighbors.Add(new Neighbor(getVertexIndex(hit4.transform.gameObject),hit4.transform.gameObject));
                }
            }
        }
    }

    public void printGraph() {
        foreach(Vertex v in vertices) {
            Debug.Log(v.vertex.name + " : Neighbors: ");
            for (int i = 0; i < v.Neighbors.Count; i++) {
                Debug.Log(v.Neighbors[i] + "Position: " + v.Neighbors[i].vertex.transform.position);
            }
        }
    }
    public int getVertexIndex(GameObject gameObject) {
        foreach(Vertex v in vertices) {
            if(v.vertex == gameObject) {
                return v.index;
            }
        }
        return -1;
    }
    public class Vertex
    {
        public int index;
        public GameObject vertex;
        public List<Neighbor> Neighbors = new List<Neighbor>();
        //public Neighbor neighbors; // adjacency linked lists for all vertices

        public Vertex(int index,GameObject vertex)
        {
            this.index = index;
            this.vertex = vertex;
        }
    }
    public class Neighbor {

        public int index;
        public GameObject vertex;
        public Neighbor(int index, GameObject vertex) {
            this.index = index;
            this.vertex = vertex;
        }
    }
}
