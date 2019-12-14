using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{              // step 4 : dfs on graph list to get each cluster and its average
    class DFS
    {  
        private int NumOFnodes;    // Exact(1)
        private int [] IsVisited;  // Exact(1)
        private Stack<int> DFStack;   // Exact(1)
        private List<RgbPixel> _distinctColors;   // Exact(1)
        private List<int>[] MST_Graph;   // Exact(1)

        public DFS(int NumOFnodes , List<int>[] MST_Graph , List<RgbPixel> colors) 
        {
            this.NumOFnodes = NumOFnodes; // Exact(1)
            IsVisited = new int[NumOFnodes]; // Exact(1)
            DFStack = new Stack<int>(NumOFnodes); // Exact(1)
            // Exact(NumOFnodes)
            for (int i = 0; i < NumOFnodes; i++)
                IsVisited[i] = 0;  // Exact(1)

            _distinctColors = new List<RgbPixel>(colors); // Exact(1)
            this.MST_Graph = MST_Graph; // Exact(1)

        }
        int numOfConnectedComponents = 0;  // Exact(1)
        int RSum = 0, BSum = 0, GSum = 0;   // Exact(1)

        public RgbPixel[] Get_Palette(int k)
        {
            RgbPixel[] Palette = new RgbPixel[k]; // Exact(1)
            int indx = 0;   // Exact(1)
            for (int i = 0; i < NumOFnodes; i++)  // Exact(NumOFnodes) * Body
            {
                if (IsVisited[i] == 0) // Exact(1)
                {
                    DepthFirstSearch(i); 
                    // Exact(1)
                    Palette[indx] = new RgbPixel(Convert.ToByte(RSum / numOfConnectedComponents), Convert.ToByte(GSum / numOfConnectedComponents), Convert.ToByte(BSum / numOfConnectedComponents));
                    indx++;  // Exact(1)
                    RSum = BSum = GSum = numOfConnectedComponents = 0;  // Exact(1)
                }
            }
            return Palette;  // Exact(1)
        }
        // Exact(1)
        public void DepthFirstSearch(int Node)
        {
            DFStack.Push(Node); // Exact(1)
            IsVisited[Node] = 1;  // Exact(1)
            while (DFStack.Count!=0)    // Exact(V) * Body
            {
                int CurrentNode = DFStack.Pop(); // Exact(1)
                numOfConnectedComponents++;   // Exact(1)
                RSum += _distinctColors[CurrentNode].red;  // Exact(1)
                BSum += _distinctColors[CurrentNode].blue;   // Exact(1)
                GSum += _distinctColors[CurrentNode].green;   // Exact(1)
                for (int j = 0; j < MST_Graph[CurrentNode].Count; j++)  // Exact(E)
                {
                    if (IsVisited[MST_Graph[CurrentNode][j]] == 0)   // Exact(1)
                        DFStack.Push(MST_Graph[CurrentNode][j]);    // Exact(1)
                } 
                IsVisited[CurrentNode] = 2;   // Exact(1)
            }
            //IsVisited[Node] = 1;
            //numOfConnectedComponents++;
            //RSum += _distinctColors[Node].red;
            //BSum += _distinctColors[Node].blue;
            //GSum += _distinctColors[Node].green;

            //for (int j = 0; j < MST_Graph[Node].Count ; j++ )
            //    if (IsVisited[MST_Graph[Node][j]] == 0)
            //        DepthFirstSearch(MST_Graph[Node][j]);

            
        }
    }
}