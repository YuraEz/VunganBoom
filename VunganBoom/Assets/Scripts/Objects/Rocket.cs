using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Rocket : MonoBehaviour
{
    public ParticleSystem flame;
    public Color color;
  //  public GameObject spawnPart;
    [SerializeField] private AudioClip clip;


    private ScoreManager scoreManager;
    private GamingManager gamingManager;
    private JoystickController joystickController;
    private Joystick joystick;

    private Rigidbody rb;

    public bool isFly = false;

    public bool isGolden = false;


    private void Start()
    {
        scoreManager = ServiceLocator.GetService<ScoreManager>();
        gamingManager = ServiceLocator.GetService<GamingManager>();
        joystickController = ServiceLocator.GetService<JoystickController>();
        joystick = joystickController.moveJoystick;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isFly) return;
        // �������� �������� �� ���������
        Vector3 movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        // ���� ���� ��������
        if (movement.sqrMagnitude > 0)
        {
            // ����������� ������ ��������
            movement.Normalize();

            // ��������� ����� ���������
            Vector3 newPosition = transform.position + movement * joystickController.moveSpeed * Time.deltaTime;

            // ��������� ������� �������
            transform.position = newPosition;

            // ��������� ����������� �������
            // Quaternion lookRotation = Quaternion.LookRotation(movement);

            // ������ ������� ������ � ����������� ��������
            // joystickController.transformRig.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * joystickController.rotationSpeed);

            // ��������� ����������� ������� �� ��� Z
            float angle = joystick.Horizontal * joystickController.rotationSpeed * 900.0f;

            // �������� ������� ���� �� ��� Z
            float currentAngle = transform.eulerAngles.z;

            // ��������� ����
            float newAngle = currentAngle + angle * Time.deltaTime;

            // ������� �������� ������ ��� Z
            Quaternion lookRotation = Quaternion.Euler(0, 0, -newAngle);

            // ������ ������� ������ � ����������� ��������, ������ �� ��� Z
            //joystickController.transformRig.rotation = lookRotation;
            joystickController.transformRig.rotation = Quaternion.Lerp(joystickController.transformRig.rotation, lookRotation, Time.deltaTime * joystickController.rotationSpeed);
        }

    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            print("��������");
        }
        // if (collision.collider.gameObject.tag == "win")
        // {
        //     ServiceLocator.GetService<UIManager>().ChangeScreen("win");
        //     scoreManager.Finish(true);
        // }
        if (collision.TryGetComponent(out Part part))
        {

            Destroy(gameObject);
            if (color == part.curColor || isGolden)
            {
                if (part.partLife - 1 > 0)
                {
                    part.partBroke.SetActive(true);
                    part.partLife -= 1;
                }
                else if (part.partLife - 1 == 0)
                {
                    scoreManager.ChangeValue(25);
                    if (part.hasAbility)
                    {
                        //���� ��������
                        print("Use ability");
                        gamingManager.AddAbility();
                        // CircleManager.instance.UseAbility(transform.position);
                    }
                    part.gameObject.SetActive(false);
                    PlayerPrefs.SetInt("Goal1", PlayerPrefs.GetInt("Goal1", 0) + 1);
                    //goalamount1
                }
                if (clip) ServiceLocator.GetService<AudioManager>().PlaySound(clip);
            }
            else
            {
                gamingManager.ChangeLives(-1);
            }
            if (clip) ServiceLocator.GetService<AudioManager>().PlaySound(clip);
        }
       
    }



    public void SetColor(Color color)
    {
        this.color = color;
        //GetComponent<SpriteRenderer>().color = color;
    }
}
