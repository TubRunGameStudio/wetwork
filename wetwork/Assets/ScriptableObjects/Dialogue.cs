using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Line
{
    public string character;
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<Line> lines;

}
