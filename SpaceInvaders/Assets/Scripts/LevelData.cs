using UnityEngine;

/*
 * Describes each individual level of a playthrough
 */

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public float LevelWidth = 2f;
    public int TotalRows = 4;
    public int UnitsPerRow = 4;
    public float SpaceBetweenUnits = 0.3f;
    public int StartRowPosition = 4;
}
