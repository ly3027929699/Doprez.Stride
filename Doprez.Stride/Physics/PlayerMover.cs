﻿using Doprez.Stride.Extensions;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doprez.Stride.Physics
{
    [ComponentCategory("Physics")]
    public class PlayerMover : StartupScript
	{
        //Camera stuff
        public float MouseSpeed;
        public float MaxCameraAngle;
        public float MinCameraAngle;

        //Player stuff
        public float MovementSpeed;
        public float JumpPower;
        
        //References
        public Entity CameraPivot;

        private Vector3 _cameraRotation;
        private CharacterComponent _character;

        public override void Start()
        {
            _character = Entity.Get<CharacterComponent>();
            if( _character == null )
			{
                _character = new CharacterComponent();
			}

            MinCameraAngle = MathUtil.DegreesToRadians(MinCameraAngle);
            MaxCameraAngle = MathUtil.DegreesToRadians(MaxCameraAngle);
            _cameraRotation = CameraPivot.Transform.RotationEulerXYZ;
        }

		/// <summary>
		/// makes the player jump.
		/// <para>power is determined by the JumpPower variable and uses the CharacterComponent.IsGrounded variable to validate if player can jump.</para>
		/// </summary>
		public void Jump()
        {
            if (_character.IsGrounded)
            {
                float jump = 0;
                jump += JumpPower;
                _character.JumpSpeed = jump;
                _character.Jump();
            }
        }

		/// <summary>
		/// Moves the player based on local rotation.
		/// <para>speed is determined by the MovementSpeed variable.</para>
		/// </summary>
		/// <param name="moveDirection"></param>
		public void MovePlayer(Vector2 moveDirection)
        {
            var velocity = new Vector3(moveDirection.X, 0, moveDirection.Y);
            velocity.Normalize();
            velocity = Vector3.Transform(velocity * MovementSpeed * this.DeltaTime(), Entity.Transform.Rotation);

            _character.SetVelocity(velocity);
        }

		/// <summary>
		/// Rotates the camera transform
		/// <para>rotation speed is determined by MouseSpeed and clamps are determined by MinCameraAngle and MaxCameraAngle</para>
		/// </summary>
		/// <param name="mouseMovement"></param>
		public void UpdateCameraRotation(Vector2 mouseMovement)
        {
            mouseMovement = mouseMovement * MouseSpeed * this.DeltaTime();

            _cameraRotation.Y -= mouseMovement.X;
            _cameraRotation.X -= mouseMovement.Y;
            _cameraRotation.X = MathUtil.Clamp(_cameraRotation.X, MinCameraAngle, MaxCameraAngle);

            //in order for the model to properly rotate we update the charactercomponent instead of the entity
            _character.Orientation = Quaternion.RotationY(_cameraRotation.Y);

            //for vertical rotation we only update the camera pivot
            CameraPivot.Transform.Rotation = Quaternion.RotationX(_cameraRotation.X);
        }
    }
}
