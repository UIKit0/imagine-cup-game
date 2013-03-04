﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using VertexArmy.Graphics;

namespace VertexArmy.States
{
	class GameStateModelViewer : IGameState
	{
		private ContentManager _cm;
		private RobotSceneNode _node;
		private SceneManager _sceneManager;
		public GameStateModelViewer( ContentManager content )
		{
			_cm = content;
			
		}

		public void OnUpdate( GameTime dt )
		{
			_node.OnUpdate( dt.ElapsedGameTime.Milliseconds );
		}

		public void OnRender( GameTime dt )
		{
			Renderer.Instance.LoadMatrix( EMatrix.World, _node.GetAbsoluteTransformation( ) );
			
			_node.OnRender(dt.ElapsedGameTime.Milliseconds, _sceneManager, 0);
		}

		public void OnEnter()
		{
			//Global.Platform.Instance.Device.RasterizerState = new RasterizerState { CullMode = CullMode.None, FillMode = FillMode.WireFrame};
		
			Renderer.Instance.LoadMatrix( EMatrix.Projection, Matrix.CreatePerspectiveFieldOfView( MathHelper.PiOver4, Global.Platform.Instance.Device.Viewport.AspectRatio, 1, 10000 ) );
			Renderer.Instance.LoadMatrix( EMatrix.View, Matrix.CreateLookAt( new Vector3( 0, 0, -300 ), new Vector3( 0, 0, 0 ), new Vector3( 0, 1, 0 ) ) );


			_node = new RobotSceneNode( );
			_node.LoadNode( _cm );
			//_node.SetScale(new Vector3(0.05f, 0.05f, 0.05f));
		}

		public void OnClose()
		{
			
		}
	}
}
