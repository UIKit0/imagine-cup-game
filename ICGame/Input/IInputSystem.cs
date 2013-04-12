﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace VertexArmy.Input
{
	public interface IInputSystem
	{
		void Update( GameTime gameTime );

		bool IsLeftPointerPressed { get; }
		bool IsRightPointerPressed { get; }

		bool IsLeftPointerFirstTimePressed { get; }
		bool IsRightPointerFirstTimePressed { get; }

		Vector2 PointerPosition { get; }
		Vector2 PointerDelta { get; }

		bool IsKeyPressed( Keys key, bool continuous = true );
	}
}
