﻿
using System;
using Microsoft.Xna.Framework;
using VertexArmy.Global;
using VertexArmy.Global.Updaters;
using VertexArmy.Graphics;

namespace VertexArmy.States
{
	internal abstract class PlayableGameState : PausableGameState
	{
		public override void OnUpdate( GameTime gameTime )
		{
			base.OnUpdate( gameTime );

			Platform.Instance.PhysicsWorld.Step( Math.Min( ( float ) gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f, ( 1f / 30f ) ) );

			TransformableControllerUpdater.Instance.Update( gameTime );
		}
		public override void OnRender( GameTime gameTime )
		{
			SceneManager.Instance.Render( gameTime.ElapsedGameTime.Milliseconds );
		}
	}
}
