using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public enum BuildingType {
    Barracks,
    TankFactory,
}

public abstract class Building {
    public int BuildingLevel;
    float? _beginConstructionTime;
    float? _secondsSinceConstruction;
    const float ConstructionTime = 360000;

    /**
     *
     */
    public void StartConstruction() {
        _beginConstructionTime = Time.realtimeSinceStartup;
    }

    /**
     *
     */
    public IEnumerator CheckConstruction() {
        while (true) {
            if (_secondsSinceConstruction >= ConstructionTime) {
                Debug.Log("Finished building");
                BuildingLevel++;
                _beginConstructionTime = null;
                _secondsSinceConstruction = null;
                yield break;
            }
            _secondsSinceConstruction = Time.realtimeSinceStartup - _beginConstructionTime;
            yield return new WaitForSeconds(1f);
        }
    }

    /**
     *
     */
    public IEnumerator GetTimeLeft(Label remainingTimeLabel) {
        if (_secondsSinceConstruction == null) yield break;
        float timeLeft = ConstructionTime - (float) _secondsSinceConstruction;
        while (timeLeft >= 0) {
            timeLeft = ConstructionTime - (float) _secondsSinceConstruction;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
            remainingTimeLabel.text = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            Debug.Log(remainingTimeLabel.text);
            yield return new WaitForSeconds(1);
        }
        remainingTimeLabel.text = $"Not construction";
    }
}