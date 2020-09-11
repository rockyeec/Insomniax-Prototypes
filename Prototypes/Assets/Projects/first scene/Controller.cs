using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    Camera cam;
    Vector3 destination;

    AudioSource audS;
    Rigidbody rb;

    [SerializeField] private AudioClip fireClip = null;

    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform firePoint = null;
    [SerializeField] private JoystickScript leftJoy = null;
    [SerializeField] private JoystickScript rightJoy = null;
    [SerializeField] private Button[] fireButtons = null;

    float xEul = 0.0f;
    float yEul = 0.0f;

    float hor = 0.0f;
    float ver = 0.0f;
    Quaternion rot;
    bool isGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audS = gameObject.AddComponent<AudioSource>();

        cam = Camera.main;
        destination = transform.position;

        foreach (Button button in fireButtons)
        {
            button.onClick.AddListener(Shoot);
        }
    }

    void Update()
    {
        float delta = Time.deltaTime;

        RecoilRecover();
        GetWalkInput(delta);
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        CheckGround();         
        LookAround(delta);
        WalkAround(delta);
    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position + 0.5f * Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.69f))
        {
            transform.position = hit.point;
            Vector3 rbVel = rb.velocity;
            rbVel.y = 0.0f;
            rb.velocity = rbVel;
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    void Shoot()
    {
        Quaternion bulletRot = firePoint.rotation;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            bulletRot = Quaternion.LookRotation(hit.point - firePoint.position);
        }

        Instantiate(bullet, firePoint.position, bulletRot);
        firePoint.parent.localRotation = Quaternion.Euler(-36.9f, 0.0f, 0.0f);
        audS.PlayOneShot(fireClip);
        Vibrator.Vibrate();
    }

    void RecoilRecover()
    {
        firePoint.parent.localRotation = Quaternion.Slerp(firePoint.parent.localRotation, Quaternion.identity, Time.deltaTime * 6.9f);
    }

    void GetWalkInput(float delta)
    {
        hor = Mathf.MoveTowards(hor, leftJoy.GetHorizontal(), 6.9f * delta);
        ver = Mathf.MoveTowards(ver, leftJoy.GetVertical()  , 6.9f * delta);

        
        rb.drag = (hor == 0.0f && ver == 0.0f ? 4.0f : 0.0f);
        
    }

    void WalkAround(float delta)
    {
        float speed = 169.69f;

        Vector3 zeroLift = transform.eulerAngles;
        zeroLift.x = 0.0f;
        rot = Quaternion.Euler(zeroLift);

        float localHor = hor * speed * delta;
        float localVer = ver * speed * delta;

        rb.velocity = rot * new Vector3(localHor, isGround ? 0.0f : rb.velocity.y, localVer);
    }

    void LookAround(float delta)
    {
        float speed = 36.9f;

        float hor = rightJoy.GetHorizontalDelta();
        float ver = rightJoy.GetVerticalDelta();

        yEul += hor * delta * speed;
        xEul += ver * delta * speed;
        xEul = Mathf.Clamp(xEul, -69.0f, 69.0f);

        cam.transform.localRotation = Quaternion.Euler(-xEul, 0.0f, 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, yEul, 0.0f);
    }

    void TouchToGo()
    {
        float speed = 6.9f;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            Ray ray = cam.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                destination = hitInfo.point;
            }
        }

        transform.position = 
            Vector3.MoveTowards(
                transform.position,
                destination,
                Time.deltaTime * speed);
    }
}
