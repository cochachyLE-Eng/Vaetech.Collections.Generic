namespace Vaetech.Collections.Generic
{
    public struct Range
    {
        public Range(int position, int count)
        {
            Position = position;
            Count = count;            
        }
        public int Position { get; set; }
        public int Count { get; set; }
    }
}
