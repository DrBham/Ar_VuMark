using UnityEngine;

public class LogoInteraction : MonoBehaviour
{
    [Header("Link Instagram")]
    public string instagramUrl = "https://www.instagram.com/anomali_jokgen?utm_source=qr&xtok=eGVobHlraGJnMXZ3";

    [Header("Putar")]
    public float rotateSpeed = 0.4f;   // derajat per piksel geser
    public float autoSpin = 0f;        // putaran otomatis (derajat/detik), 0 = mati

    [Header("Ketuk")]
    public float tapMaxMove = 25f;     // piksel
    public float tapMaxTime = 0.35f;   // detik

    Camera cam;
    Collider col;
    float yaw;
    bool dragging;
    Vector2 startPos;
    float startTime;

    void Start()
    {
        cam = Camera.main;
        col = GetComponentInChildren<Collider>();
    }

    void Update()
    {
        if (cam == null) cam = Camera.main;
        if (cam == null || col == null || Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);

        switch (t.phase)
        {
            case TouchPhase.Began:
                if (HitsLogo(t.position))
                {
                    dragging = true;
                    startPos = t.position;
                    startTime = Time.time;
                }
                break;

            case TouchPhase.Moved:
                if (dragging) yaw -= t.deltaPosition.x * rotateSpeed;
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (dragging)
                {
                    bool isTap = (t.position - startPos).magnitude < tapMaxMove
                                 && (Time.time - startTime) < tapMaxTime;
                    if (isTap) Application.OpenURL(instagramUrl);
                    dragging = false;
                }
                break;
        }
    }

    void LateUpdate()
    {
        if (cam == null) return;
        if (autoSpin != 0f && !dragging) yaw += autoSpin * Time.deltaTime;

        // menghadap kamera, lalu diputar sebesar yaw seperti kartu dibalik
        transform.rotation = cam.transform.rotation * Quaternion.Euler(0f, yaw, 0f);
    }

    bool HitsLogo(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        return col.Raycast(ray, out _, 10f);
    }
}