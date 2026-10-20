using UnityEngine;

public class PanelFollow : MonoBehaviour
{
    public Transform anchor;                                    // isi dengan LogoPivot
    public Vector3 cameraOffset = new Vector3(0.12f, 0f, 0f);   // kanan, atas, depan (relatif kamera)

    Camera cam;

    void LateUpdate()
    {
        if (cam == null) cam = Camera.main;
        if (cam == null || anchor == null) return;

        Transform c = cam.transform;
        transform.position = anchor.position
                           + c.right * cameraOffset.x
                           + c.up * cameraOffset.y
                           + c.forward * cameraOffset.z;
        transform.rotation = c.rotation;   // selalu menghadap kamera, sejajar dengan logo
    }
}