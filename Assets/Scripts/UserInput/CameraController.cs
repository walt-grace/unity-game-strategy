using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Camera _camera;
    const float MoveSpeed = 5f;
    const float ZoomSpeed = 5f;
    const float MinZoom = 2f;
    const float MaxZoom = 100f;

    void Start() {
        _camera = Camera.main;
    }

    void Update() {
        // Camera movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new(horizontalInput, verticalInput, 0f);
        moveDirection.Normalize();
        transform.Translate(moveDirection * (MoveSpeed * Time.deltaTime));
        // Camera zoom
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = _camera.orthographicSize - scrollWheelInput * ZoomSpeed;
        newZoom = Mathf.Clamp(newZoom, MinZoom, MaxZoom);
        _camera.orthographicSize = newZoom;
    }
}