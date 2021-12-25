using System;

public class Coord : IComparable<Coord>, IEquatable<Coord>
{
    public int ID = 0;
    public float X = 0;
    public float Y = 0;
    public float Z = 0;

    public int CompareTo(Coord? other)
    {
        if (other == null)
            return 1;
        
        return this.ID.CompareTo(other.ID);
    }

    public bool Equals(Coord? other)
    {
        if (other == null)
            return false;
        
        return this.ID == other.ID;
    }
}