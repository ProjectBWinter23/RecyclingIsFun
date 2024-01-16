using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45f;

    void Update()
    {
        // Rotate the UI image around its center
        RotateUIImage();
    }

    private void RotateUIImage()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}