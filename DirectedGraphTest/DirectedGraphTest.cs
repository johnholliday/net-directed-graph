using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectedGraph;

namespace DirectedGraphTest
{
    [TestClass]
    public class DirectedGraphTest
    {
        /// <summary>
        /// Test DirectedGraph. 
        /// TODO: refactor this method into separate test methods for easier understanding and maintenance.
        /// </summary>
        [TestMethod]
        public void TestDirectedGraph()
        {
            DirectedGraph<string, int> graph = new DirectedGraph<string, int>();
            
            // add several nodes
            graph.AddNode("A", "B", 5);
            graph.AddNode("B", "C", 4);
            graph.AddNode("C", "D", 8);
            graph.AddNode("D", "C", 8);
            graph.AddNode("D", "E", 6);
            graph.AddNode("A", "D", 5);
            graph.AddNode("C", "E", 2);
            graph.AddNode("E", "B", 3);
            graph.AddNode("A", "E", 7);

            // test duplicate
            try
            {
                graph.AddNode("A", "B", 5);
                Assert.Fail("ArgumentException not thrown.");
            }
            catch(ArgumentException)
            {
            }

            // test search

            LinkedList<LinkedList<Node<string, int>>> results = graph.Search("A", "B", 1);
            printResults("A-B (Depth <= 1)", results);
            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(2, results[0].Length);
            Assert.AreEqual("B", results[0][1].Value);

            results = graph.Search("A", "C", 4);
            printResults("A-C (Depth <= 4)", results);

            results = graph.Search("A", "D", cycle:false);
            printResults("A-D", results);

            results = graph.Search("B", "B");
            printResults("B-B", results);

            results = graph.Search("C", "D", depth: 5);
            printResults("C-D (Depth <= 5)", results);

            // test search with weight and cycle

            results = graph.Search("C", "C", weight: 30, cycle: true);
            Assert.AreEqual(7, results.Length);
            printResults("C-C (Weight < 30)", results);

        }

        /// <summary>
        /// Helper method to print the results of the search to test output.
        /// Useful debug aid.
        /// </summary>
        /// <param name="text">Any text to output.</param>
        /// <param name="results">The search results.</param>
        void printResults(string text, LinkedList<LinkedList<Node<string, int>>> results)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine(text);
            Console.WriteLine("--------------------");
            foreach (LinkedList<Node<string, int>> result in results)
            {
                int weight = 0;
                foreach (Node<string, int> node in result)
                {
                    weight += node.Weight;
                    Console.Write(node.ToString());
                }
                Console.WriteLine(" (Weight = " + weight + ")");
            }
        }
    }
}
