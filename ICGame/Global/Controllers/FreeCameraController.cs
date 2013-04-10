﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using VertexArmy.Global.Behaviours;
using VertexArmy.Graphics.Attachables;

namespace VertexArmy.Global.Controllers
{
	public class FreeCameraController : IController
	{
		private Vector3 _delta = Vector3.Zero;
		private Vector3 _screen;
		private Vector2 _error, _error2;
		private float _lastScrollValue;
		public FreeCameraController( CameraAttachable camera )
		{
			Data = new List<object> { camera };
			_screen = new Vector3( Platform.Instance.DeviceManager.PreferredBackBufferWidth, Platform.Instance.DeviceManager.PreferredBackBufferHeight, 0 );
			_screen /= 2;
			_error = new Vector2( _screen.X * 0.85f, _screen.Y * 0.85f );
			_error2 = new Vector2( _screen.X, _screen.Y );
			_lastScrollValue = Mouse.GetState().ScrollWheelValue;
		}

		public void Update( GameTime dt )
		{
			CameraAttachable camera = Data[0] as CameraAttachable;

			bool ok = ( camera != null );

			if ( !ok ) return;

			if ( !Platform.Instance.Game.IsActive )
			{
				return;
			}

			if ( Mouse.GetState().ScrollWheelValue != _lastScrollValue )
			{
				camera.Parent.SetPosition( camera.Parent.GetPosition() - ( Mouse.GetState().ScrollWheelValue - _lastScrollValue ) * Vector3.UnitZ );
				_lastScrollValue = Mouse.GetState().ScrollWheelValue;
			}

			Vector3 direction = new Vector3( Mouse.GetState().X, Mouse.GetState().Y, 0f );
			direction -= _screen;

			if ( Math.Abs( direction.X ) < _error.X && Math.Abs( direction.Y ) < _error.Y )
			{
				return;
			}
			if ( Math.Abs( direction.X ) > _error2.X || Math.Abs( direction.Y ) > _error2.Y )
			{
				return;
			}


			direction *= new Vector3( 1f, -1f, 0f );
			direction *= ( float ) dt.ElapsedGameTime.TotalSeconds;

			Vector3 lookingPosition = camera.Parent.GetPosition() + direction;
			camera.Parent.SetPosition( lookingPosition );
		}


		public List<object> Data { get; set; }

		public void Clean()
		{

		}
	}
}