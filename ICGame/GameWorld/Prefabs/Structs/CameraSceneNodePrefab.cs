using Microsoft.Xna.Framework;

namespace VertexArmy.GameWorld.Prefabs.Structs
{
	public struct CameraSceneNodePrefab
	{
		public Vector3 LookingDirection;
		public Vector3 UpVector;
		public float Near, Far, AspectRatio, Fov;
	}
}