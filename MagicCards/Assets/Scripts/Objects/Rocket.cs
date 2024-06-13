using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Rocket : MonoBehaviour
{
    public Color color;
  //  public GameObject spawnPart;
    [SerializeField] private AudioClip clip;


    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = ServiceLocator.GetService<ScoreManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Ball")
        {
            print("Коснулся");
        }
        // if (collision.collider.gameObject.tag == "win")
        // {
        //     ServiceLocator.GetService<UIManager>().ChangeScreen("win");
        //     scoreManager.Finish(true);
        // }
        if (collision.collider.TryGetComponent(out Part part))
        {

           

            Destroy(gameObject);
            if (color == part.curColor)
            {
                if (part.partLife - 1 > 0)
                {
                    part.partBroke.SetActive(true);
                    part.partLife -=  1;
                }
                else if (part.partLife - 1 == 0)
                {
                    scoreManager.UpdateGame(20);
                    if (part.hasAbility)
                    {
                        //Если способка
                        print("Use ability");
                       // CircleManager.instance.UseAbility(transform.position);
                    }
                    part.gameObject.SetActive(false);
                    PlayerPrefs.SetInt("goalamount1", PlayerPrefs.GetInt("goalamount1", 0) + 1);
                    //goalamount1
                }
                //if (clip) ServiceLocator.GetService<AudioManager>().PlaySound(clip);
            }
            else
            {
                CircleManager.instance.AddPart(part, color);
            }
            
        }
        //if (clip) ServiceLocator.GetService<AudioManager>().PlaySound(clip);
    }

    public void SetColor(Color color)
    {
        this.color = color;
        //GetComponent<SpriteRenderer>().color = color;
    }
}
