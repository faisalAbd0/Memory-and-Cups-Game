using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinates
{
    public int box1;
    public int box2;
    public int img1;
    public int img2;

    public Coordinates(int box1, int img1, int box2, int img2)
    {
        this.box1 = box1;
        this.box2 = box2;
        this.img1 = img1;
        this.img2 = img2;
    }
    public override bool Equals(object obj)
    {
        Coordinates other = obj as Coordinates;
        if (other == null) return false;
        else
            return this.box1 == other.box1 && this.box2 == other.box2
                && this.img1 == other.img1 && this.img2 == other.img2;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(box1, box2, img1, img2);
    }
}
