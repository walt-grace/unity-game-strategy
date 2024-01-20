using System.Collections;
using UnityEngine;

public enum BuildingType {
    Barracks,
    TankFactory,
}

public class Building {
    public int BuildingLevel;
    public float? BeginConstructionTime;
    public float ConstructionTime = 5;
    public float ConstructionTimeLeft;

    /**
     *
     */
    public void StartConstruction() {
        BeginConstructionTime = Time.realtimeSinceStartup;
    }

    /**
     *
     */
    public IEnumerator UpdateConstruction() {
        while (BeginConstructionTime != null) {
            ConstructionTimeLeft = ConstructionTime - Time.realtimeSinceStartup - (float) BeginConstructionTime;
            if (ConstructionTimeLeft >= 0) {
                BuildingLevel++;
                BeginConstructionTime = null;
                ConstructionTime += 5;
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}