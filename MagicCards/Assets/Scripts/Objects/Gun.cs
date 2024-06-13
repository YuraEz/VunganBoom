using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject ballPref;
    [SerializeField] private float shotForece;
    [SerializeField] private Button shotBtn;
    [ReadOnly, SerializeField] private Rocket curBall;

    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    [SerializeField] private List<Color> colors;
    [SerializeField] private List<Sprite> colorSprites;
    [SerializeField] private int colorIndex;

    [SerializeField] private float shotDelay;
    private bool canShoot = true;
    void Start()
    {
        shotBtn.onClick.AddListener(Shot);
        PlaceNewBall();
    }

    public void PlaceNewBall()
    {
        if (curBall)
        {
            Destroy(curBall.gameObject);
        }

        curBall = Instantiate(ballPref, point1.position, Quaternion.identity).GetComponent<Rocket>();
        curBall.GetComponent<Collider2D>().isTrigger = true;
        int index = Random.Range(0, colors.Count);
        curBall.SetColor(colors[index]);
        curBall.GetComponent<SpriteRenderer>().sprite = colorSprites[index];
    }

    private void Shot()
    {
        if (!canShoot) return;
        GameObject newBall = Instantiate(ballPref, point1.position, Quaternion.identity);
        Vector2 dir = point2.position - point1.position;
        newBall.GetComponent<Rocket>().SetColor(curBall.color);
        newBall.GetComponent<SpriteRenderer>().sprite = curBall.GetComponent<SpriteRenderer>().sprite;

        newBall.GetComponent<Rigidbody2D>().AddForce(dir.normalized * shotForece, ForceMode2D.Impulse);
        Destroy(newBall, 5);
        PlaceNewBall();
        canShoot = false;
        shotBtn.interactable = false;
        Invoke("CanShot", shotDelay);
    }

    private float timeCounter = 0f;  // Счётчик времени
    private bool rotateClockwise = true; // Флаг для направления вращения
    public TextMeshProUGUI timerText; // Ссылка на TextMesh Pro компонент для отображения времени
    private float timerValue = 3f; // Значение таймера

    private void Update()
    {
        //transform.eulerAngles = transform.eulerAngles+ new Vector3(0, 0, Time.deltaTime * rotateSpeed);
        // Обновляем счётчик времени
        timeCounter += Time.deltaTime;

        // Уменьшаем значение таймера каждую секунду
        if (timeCounter >= 1f)
        {
            timerValue -= 1f;
            timeCounter = 0f;
        }

        // Обновляем текст таймера
        //timerText.text = timerValue.ToString("0");
        timerText.text = "00:" + timerValue.ToString("00");

        // Проверяем, если таймер достиг 0
        if (timerValue <= 0f)
        {
            // Сбрасываем значение таймера на 10
            timerValue = 3f;

            // Переключаем направление вращения
            //rotateClockwise = !rotateClockwise;
            Shot();
        }

    }

    private void CanShot()
    {
        canShoot = true;
        shotBtn.interactable = true;
    }
}
