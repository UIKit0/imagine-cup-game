﻿using VertexArmy.Global.Behaviours;
using VertexArmy.Global.Hints;
using VertexArmy.Global.Managers;


namespace VertexArmy.Global.ShapeListeners
{
	class HintShapeListener : IShapeListener
	{
		private readonly FadeHint _hint;
		private readonly bool _onlyOnce;

		private int _timesPlayed;

		public HintShapeListener ( FadeHint hint )
		{
			_timesPlayed = 0;
			_onlyOnce = false;
			_hint = hint;
			HintManager.Instance.SpawnHint( hint );
		}

		public HintShapeListener( FadeHint hint, bool onlyOnce )
		{
			_hint = hint;
			_onlyOnce = onlyOnce;
			HintManager.Instance.SpawnHint( hint );
		}

		public void OnEnterShape()
		{
			if( _onlyOnce && _timesPlayed == 0 )
				_hint.StartAsync();
			else if (!_onlyOnce)
				_hint.StartAsync();

			_timesPlayed++;
		}

		public void OnExitShape()
		{
			_hint.StopHintAsync();
		}

		public void OnEachFrameInsideShape()
		{
			
		}
	}
}
