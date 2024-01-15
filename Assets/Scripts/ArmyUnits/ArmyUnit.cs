using System.Collections;
using UnityEngine;

public class ArmyUnit : MonoBehaviour, ISelectable {
    SpriteRenderer _sprite;
    float _moveSpeed = 6f;
    bool _isMoving;

    void Start() {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void ApplySelection() {
        _sprite.color = Color.green;
    }

    public void RemoveSelection() {
        _sprite.color = Color.white;
    }

    public UIType GetUIType() {
        return UIType.ArmyUnit;
    }

    /**
     *
     */
    public IEnumerator MoveUnit(Vector3 targetPosition) {
        targetPosition.z = 0f;
        while (Vector3.Distance(transform.position, targetPosition) >= 0.01f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
