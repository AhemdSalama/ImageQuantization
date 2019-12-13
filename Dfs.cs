Class Dfs
{
  private int _vertixSize;
  private bool[] _visited;
  private list<RgbPixel>_distinctColors;
  Dfs(int n,list<RgbPixel>colors)
  {
    _vertixSize = n;
    _visited = new bool[n]; // no need to intialize it with false
    _distinctColors = new list<RgbPixel>(colors);
  }

  private void DfsTraverse(int v)
  {
    // witre your code here for the Dfs
  }

  public int[] Pallet(list<int>[]graph,int k)
  {
    int[] pallet = new int[k];
    int numOfConnectedComponents=0;
    double avg =0d;
    for(int i=0;i<_vertixSize;i++)
    {
      if(!_visited[i])
      {
        DfsTraverse(i);
      }
    }
  }
}
