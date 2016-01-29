using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    abstract class LocomotionsInterface
    {
        #region properties
        private GameObject gameObject;
        public GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        private Rigidbody rigidbody;
        public Rigidbody Rigidbody
        {
            get { return rigidbody; }
            set { rigidbody = value; }
        }

        private float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private float movementVertical;
        public float MovementVertical
        {
            get { return movementVertical; }
            set { movementVertical = value; }
        }
        private float movementHorizontal;
        public float MovementHorizontal
        {
            get { return movementHorizontal; }
            set { movementHorizontal = value; }
        }

        private float cameraRay;

        public float CameraRay
        {
            get { return cameraRay; }
            set { cameraRay = value; }
        }


        #endregion

        public LocomotionsInterface(GameObject gameObject)
        {
            this.gameObject = gameObject;
            LoadComponents();
        }
        public LocomotionsInterface(GameObject gameObject, float speed)
        {
            this.gameObject = gameObject;
            this.speed = speed;
            LoadComponents();
        }

        private void LoadComponents()
        {
            if (this.gameObject.GetComponent<Rigidbody>() != null)
                this.rigidbody = this.gameObject.GetComponent<Rigidbody>();
        }

        public abstract void Move();

        public float GetHorizontalInput()
        {
            this.movementHorizontal = Input.GetAxisRaw("Horizontal");
            //Debug.Log(movementHorizontal + " H");
            return movementHorizontal;
        }
        public float GetVerticalInput()
        {
            this.movementVertical = Input.GetAxisRaw("Vertical");
            //Debug.Log(movementVertical + " V");
            return movementVertical;
        }

        public void OnMouseMove()
        {
            //Camera.main
            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(this.GameObject.transform.position);

            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

            //Ta Daaa
            this.GameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
        }

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
        public abstract void GetInputs();
        //public abstract void Turn();
    }
}
