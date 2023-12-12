using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetShooter : MonoBehaviour
{
    [SerializeField] Camera cam;
    public TextMeshProUGUI ScoreCounter;
    private int Score = 0;
    public TextMeshProUGUI MissCounter;
    private int Miss = 0;
    public AudioSource GunShot;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            GunShot.Play();
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();
                if(target != null)
                {
                    target.Hit();
                    Score++;
                    ScoreCounter.text = Score.ToString();
                }
                else
                {
                    Miss++;
                    MissCounter.text = Miss.ToString();
                }
            }
        }
        if (Input.GetKeyDown("r"))
        {
            Score = 0;
            ScoreCounter.text = Score.ToString();
            Miss =0;
            MissCounter.text = Miss.ToString();
        }
    }
}
