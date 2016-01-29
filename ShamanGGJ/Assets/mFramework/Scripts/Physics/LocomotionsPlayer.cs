using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class LocomotionsPlayer:LocomotionsInterface
    {
        Vector3 movement = new Vector3();
        public LocomotionsPlayer(GameObject gameObject)
            : base(gameObject)
        {
        }
        public LocomotionsPlayer(GameObject gameObject,float speed)
            : base(gameObject,speed)
        {
        }

        public override void Move()
        {
            /*movement.Set(MovementHorizontal, 0.0f, MovementHorizontal);
            movement = movement.normalized * Speed * Time.deltaTime;
            Rigidbody.MovePosition(this.GameObject.transform.position + movement);*/
            movement.Set(this.MovementHorizontal, 0.0f, this.MovementVertical);
            movement = movement.normalized * Speed * Time.deltaTime;
            Rigidbody.MovePosition(GameObject.transform.position + movement);
        }
        
        public override void GetInputs()
        {
            GetHorizontalInput();
            GetVerticalInput();
        }
 
    }
}
