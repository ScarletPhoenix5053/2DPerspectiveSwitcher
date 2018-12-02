using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sierra.PerspectiveSwitcher2D
{
    /// <summary>
    /// Main class for camera switching operation. Desgined to be attatched to a game manager component
    /// </summary>
    public class PerspectiveSwitcher : MonoBehaviour
    {
        public Camera.CameraController Camera;
        public CubePerspective CurrentPerspective;
        public BoolCube DeniedPerspectives = new BoolCube();
        public bool FreezeTimeOnSwtich;
        public float SmoothTransitionDuration = 0.5f;

        private CubePerspective _newPerspective;
        
        public enum CubePerspective
        {
            top,
            bottom,
            left,
            right,
            back,
            front
        }

        public void SetPerspective(CubePerspective newPerspective)
        {
            // Validate perspective
            if (newPerspective == CurrentPerspective) return;
            if (!PerspectiveAllowed(newPerspective)) return;

            // Update all the things
            SetNewPerspectiveTo(newPerspective);            
            UpdateCamera();
            SetCurrentPerspectiveTo(newPerspective);
        }
        public void DenyPerspective(BoolCube boolCube)
        {
            if (boolCube.Top) DeniedPerspectives.Top = true;
            if (boolCube.Bottom) DeniedPerspectives.Bottom = true;
            if (boolCube.Left) DeniedPerspectives.Left = true;
            if (boolCube.Right) DeniedPerspectives.Right = true;
            if (boolCube.Front) DeniedPerspectives.Front = true;
            if (boolCube.Bottom) DeniedPerspectives.Bottom = true;
        }
        public void DenyPerspective(CubePerspective newPerspective)
        {
            switch (newPerspective)
            {
                case CubePerspective.top:
                    DeniedPerspectives.Top = true;
                    break;
                case CubePerspective.bottom:
                    DeniedPerspectives.Bottom = true;
                    break;
                case CubePerspective.left:
                    DeniedPerspectives.Left = true;
                    break;
                case CubePerspective.right:
                    DeniedPerspectives.Right = true;
                    break;
                case CubePerspective.back:
                    DeniedPerspectives.Front = true;
                    break;
                case CubePerspective.front:
                    DeniedPerspectives.Bottom = true;
                    break;
                default:
                    break;
            }
        }
        public void AllowPerspective(BoolCube boolCube)
        {
            if (boolCube.Top) DeniedPerspectives.Top = false;
            if (boolCube.Bottom) DeniedPerspectives.Bottom = false;
            if (boolCube.Left) DeniedPerspectives.Left = false;
            if (boolCube.Right) DeniedPerspectives.Right = false;
            if (boolCube.Front) DeniedPerspectives.Front = false;
            if (boolCube.Bottom) DeniedPerspectives.Bottom = false;
        }
        public void AllowPerspective(CubePerspective newPerspective)
        {
            switch (newPerspective)
            {
                case CubePerspective.top:
                    DeniedPerspectives.Top = false;
                    break;
                case CubePerspective.bottom:
                    DeniedPerspectives.Bottom = false;
                    break;
                case CubePerspective.left:
                    DeniedPerspectives.Left = false;
                    break;
                case CubePerspective.right:
                    DeniedPerspectives.Right = false;
                    break;
                case CubePerspective.back:
                    DeniedPerspectives.Front = false;
                    break;
                case CubePerspective.front:
                    DeniedPerspectives.Bottom = false;
                    break;
                default:
                    break;
            }
        }

        private void UpdateCamera()
        {
            var target = Camera.LookAt.position;
            var dist = Camera.DistToSubject;

            if (TransitionIs180())
            {
                // If transition is 180 degrees: offset target to get desired direction
                // add option to default to clockwise/anticlockwise/overthetop motion?

                // DEFAULT ROTATIONS, based on clockwise rotation, looking from behind/top
                switch (CurrentPerspective)
                {
                    case CubePerspective.top:
                        Camera.PivotTo(target + Vector3.back * dist, target + new Vector3(0.1f,0,0), SmoothTransitionDuration);
                        break;
                    case CubePerspective.bottom:
                        Camera.PivotTo(target + Vector3.back * dist, target + new Vector3(-0.1f, 0, 0), SmoothTransitionDuration);
                        break;
                    case CubePerspective.left:
                        Camera.PivotTo(target + Vector3.back * dist, target + new Vector3(0, 0, -0.1f), SmoothTransitionDuration);
                        break;
                    case CubePerspective.right:
                        Camera.PivotTo(target + Vector3.back * dist, target + new Vector3(0, 0, 0.1f), SmoothTransitionDuration);
                        break;
                    case CubePerspective.back:
                        Camera.PivotTo(target + Vector3.back * dist, target + new Vector3(0.1f, 0, 0), SmoothTransitionDuration);
                        break;
                    case CubePerspective.front:
                        Camera.PivotTo(target + Vector3.back * dist, target + new Vector3(-0.1f, 0, 0), SmoothTransitionDuration);
                        break;
                    default:
                        break;
                }
            }
            else 
            {
                // If transition is 90 degrees: do not offset target
                switch (_newPerspective)
                {
                    case CubePerspective.top:
                        Camera.PivotTo(target + Vector3.back * dist, target, SmoothTransitionDuration);
                        break;
                    case CubePerspective.bottom:
                        Camera.PivotTo(target + Vector3.back * dist, target, SmoothTransitionDuration);
                        break;
                    case CubePerspective.left:
                        Camera.PivotTo(target + Vector3.back * dist, target, SmoothTransitionDuration);
                        break;
                    case CubePerspective.right:
                        Camera.PivotTo(target + Vector3.back * dist, target, SmoothTransitionDuration);
                        break;
                    case CubePerspective.back:
                        Camera.PivotTo(target + Vector3.back * dist, target, SmoothTransitionDuration);
                        break;
                    case CubePerspective.front:
                        Camera.PivotTo(target + Vector3.back * dist, target, SmoothTransitionDuration);
                        break;
                    default:
                        break;
                }
            }


        }
        private bool TransitionIs180()
        {
            return
                ((CurrentPerspective == CubePerspective.back) && (_newPerspective == CubePerspective.front)) ||
                ((CurrentPerspective == CubePerspective.front) && (_newPerspective == CubePerspective.back)) ||
                ((CurrentPerspective == CubePerspective.top) && (_newPerspective == CubePerspective.bottom)) ||
                ((CurrentPerspective == CubePerspective.bottom) && (_newPerspective == CubePerspective.top)) ||
                ((CurrentPerspective == CubePerspective.left) && (_newPerspective == CubePerspective.right)) ||
                ((CurrentPerspective == CubePerspective.right) && (_newPerspective == CubePerspective.left));
        }
        private bool PerspectiveAllowed(CubePerspective newPerspective)
        {
            switch (newPerspective)
            {
                case CubePerspective.top:
                    if (DeniedPerspectives.Top) return false;
                    break;
                case CubePerspective.bottom:
                    if (DeniedPerspectives.Bottom) return false;
                    break;
                case CubePerspective.left:
                    if (DeniedPerspectives.Left) return false;
                    break;
                case CubePerspective.right:
                    if (DeniedPerspectives.Right) return false;
                    break;
                case CubePerspective.back:
                    if (DeniedPerspectives.Back) return false;
                    break;
                case CubePerspective.front:
                    if (DeniedPerspectives.Front) return false;
                    break;
                default:
                    return false;
            }
            return true;
        }
        private void SetCurrentPerspectiveTo(CubePerspective newPerspective)
        {
            CurrentPerspective = newPerspective;
        }
        private void SetNewPerspectiveTo(CubePerspective newPerspective)
        {
            _newPerspective = newPerspective;
        }
    }

    /// <summary>
    /// Six booleans named to represent the six faces of cube.
    /// </summary>
    [Serializable]
    public class BoolCube
    {
        /// <summary>
        /// Facing -z
        /// </summary>
        public bool Front = false;
        /// <summary>
        /// Facing +z
        /// </summary>
        public bool Back = false;
        /// <summary>
        /// Facing +x
        /// </summary>
        public bool Left = false;
        /// <summary>
        /// Facing -x
        /// </summary>
        public bool Right = false;
        /// <summary>
        /// Facing -y
        /// </summary>
        public bool Top = false;
        /// <summary>
        /// Facing +y
        /// </summary>
        public bool Bottom = false;

        public BoolCube()
        {
            Front = false;
            Back = false;
            Left = false;
            Right = false;
            Top = false;
            Bottom = false;
        }
    }
}