using UnityEngine;
using System;
using Sierra.PerspectiveSwitcher2D;

public class GameManager : MonoBehaviour
{
    public PerspectiveSwitcher PerspectiveSwitcher;

    private bool CamPerspAlternate = true;

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SetRandomCamPersp();
        }
    }

    private void SetRandomCamPersp()
    {
        int newPersp = UnityEngine.Random.Range(1, 7);
        switch (newPersp)
        {
            case 1:
                PerspectiveSwitcher.SetPerspective(PerspectiveSwitcher.CubePerspective.top);
                break;
            case 2:
                PerspectiveSwitcher.SetPerspective(PerspectiveSwitcher.CubePerspective.bottom);
                break;
            case 3:
                PerspectiveSwitcher.SetPerspective(PerspectiveSwitcher.CubePerspective.left);
                break;
            case 4:
                PerspectiveSwitcher.SetPerspective(PerspectiveSwitcher.CubePerspective.right);
                break;
            case 5:
                PerspectiveSwitcher.SetPerspective(PerspectiveSwitcher.CubePerspective.front);
                break;
            case 6:
                PerspectiveSwitcher.SetPerspective(PerspectiveSwitcher.CubePerspective.back);
                break;
            default:
                break;
        }
    }
    private void SetAlternatingCamPersp(PerspectiveSwitcher.CubePerspective persp1, PerspectiveSwitcher.CubePerspective persp2)
    {        
        if (CamPerspAlternate)
        {
            PerspectiveSwitcher.SetPerspective(persp1);

            CamPerspAlternate = false;
        }
        else
        {
            PerspectiveSwitcher.SetPerspective(persp2);

            CamPerspAlternate = true;
        }
    }
}