public class NodeABB
{
    public int Value { get; set; }
    public NodeABB Left { get; set; }
    public NodeABB Right { get; set; }
    public int Height { get; set; }

    public NodeABB(int value)
    {
        this.Value = value;
        this.Left = null;
        this.Right = null;
        Height = 0;
    }
}