using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using VertexArmy.GameWorld.Prefabs;
using VertexArmy.GameWorld.Prefabs.Structs;

namespace VertexArmy.Content.Prefabs
{
	public class RobotPrefab
	{
		public static PrefabEntity CreatePrefab()
		{
			PrefabEntity robot = new PrefabEntity { Name = "Robot" };

			/* gear bodies */
			ShapePrefab gearShape = new ShapePrefab { Type = ShapeType.Circle, XRadius = 14f, Density = 1f };

			BodyPrefab gear1 = new BodyPrefab
			{
				Name = "Gear1",
				Friction = 10f,
				Restitution = 0.1f,
				LocalPosition = new Vector2( -27.93f, -20.13f ),
				Static = false,
				Shapes = new List<ShapePrefab> { gearShape },
				CollisionGroup = 69
			};

			BodyPrefab gear2 = new BodyPrefab
			{
				Name = "Gear2",
				Friction = 10f,
				Restitution = 0.1f,
				LocalPosition = new Vector2( 27.93f, -20.13f ),
				Static = false,
				Shapes = new List<ShapePrefab> { gearShape },
				CollisionGroup = 69
			};

			BodyPrefab gear3 = new BodyPrefab
			{
				Name = "Gear3",
				Friction = 10f,
				Restitution = 0.1f,
				LocalPosition = new Vector2( 0f, 28.25f ),
				Static = false,
				Shapes = new List<ShapePrefab> { gearShape },
				CollisionGroup = 69
			};

			robot.RegisterBody( gear1 );
			robot.RegisterBody( gear2 );
			robot.RegisterBody( gear3 );

			/* chassis body */
			ShapePrefab chassisShape = new ShapePrefab { Type = ShapeType.Polygon, Density = 1f, Polygon = new List<Vertices> { new Vertices() } };
			chassisShape.Polygon[0].Add( new Vector2( -15.41f, -13.87f ) );
			chassisShape.Polygon[0].Add( new Vector2( 0f, 13.74f ) );
			chassisShape.Polygon[0].Add( new Vector2( 15.41f, -13.87f ) );

			BodyPrefab chassis = new BodyPrefab
			{
				Name = "Chassis",
				Restitution = 0.1f,
				Friction = 1f,
				LocalPosition = Vector2.Zero,
				Static = false,
				Shapes = new List<ShapePrefab> { chassisShape },
				CollisionGroup = 69
			};

			robot.RegisterBody( chassis, true ); // is main body

			/* gear joints */
			const float jointLengthModifier = 1f;

			JointPrefab gearJoint1 = new JointPrefab
			{
				Type = JointType.Line,
				Name = "GearJoint1",
				Body1 = "Chassis",
				Body2 = "Gear1",
				Anchor = gear1.LocalPosition,
				Axis = new Vector2( 0.66f, -0.33f ) * jointLengthModifier,
				MaxMotorTorque = 60f,
				Frequency = 10f,
				DampingRatio = 0.85f,
				MotorEnabled = true,
			};

			JointPrefab gearJoint2 = new JointPrefab
			{
				Type = JointType.Line,
				Name = "GearJoint2",
				Body1 = "Chassis",
				Body2 = "Gear2",
				Anchor = gear2.LocalPosition,
				Axis = new Vector2( -0.66f, -0.33f ) * jointLengthModifier,
				MaxMotorTorque = 60f,
				Frequency = 10f,
				DampingRatio = 0.85f,
				MotorEnabled = true
			};

			JointPrefab gearJoint3 = new JointPrefab
			{
				Type = JointType.Line,
				Name = "GearJoint3",
				Body1 = "Chassis",
				Body2 = "Gear3",
				Anchor = gear3.LocalPosition,
				Axis = new Vector2( 0f, 0.76f ) * jointLengthModifier,
				MaxMotorTorque = 60f,
				Frequency = 10f,
				DampingRatio = 0.85f,
				MotorEnabled = true
			};

			robot.RegisterJoint( gearJoint1 );
			robot.RegisterJoint( gearJoint2 );
			robot.RegisterJoint( gearJoint3 );

			ShapePrefab linkShape = new ShapePrefab
			{
				Type = ShapeType.Rectangle,
				Width = 2.51f,
				Height = 8.88f,
				Density = 2f,
				Offset = Vector2.Zero
			};

			ShapePrefab linkFeetShape = new ShapePrefab
			{
				Type = ShapeType.Rectangle,
				Width = 2.51f,
				Height = 4f,
				Density = 2f,
				Offset = new Vector2( 2f, 0f )
			};

			BodyPrefab link = new BodyPrefab
			{

				Name = "Link",
				Shapes = new List<ShapePrefab> { linkShape, linkFeetShape },
				Friction = 0.1f,
				Restitution = 0f,
				CollisionGroup = 69
			};

			PathPrefab track = new PathPrefab
			{
				Name = "Track",
				Anchor1 = new Vector2( -1.5f, -4.4f ),
				Anchor2 = new Vector2( -1.5f, 4.4f ),
				BodyCount = 29,
				CollideConnected = false,
				ConnectFirstAndLast = true,
				JointType = JointType.Revolute,
				Path = new List<Vector2>
					{
						new Vector2(60f, -32f),
						new Vector2(0f, 51f),
						new Vector2(-50f, -32f ),
						new Vector2(55f , -38f  )
					},
				Body = link
			};

			robot.RegisterPath( track );

			/* scene nodes */
			MeshSceneNodePrefab gearNode1 = new MeshSceneNodePrefab
			{
				Name = "Gear1",
				Mesh = "models/robo_wheel",
				Material = "CelShadingMaterial",
			};

			MeshSceneNodePrefab gearNode2 = new MeshSceneNodePrefab
			{
				Name = "Gear2",
				Mesh = "models/robo_wheel",
				Material = "CelShadingMaterial",
			};

			MeshSceneNodePrefab gearNode3 = new MeshSceneNodePrefab
			{
				Name = "Gear3",
				Mesh = "models/robo_wheel",
				Material = "CelShadingMaterial",
			};

			MeshSceneNodePrefab jointest = new MeshSceneNodePrefab
			{
				Name = "linkusedasjoint",
				Mesh = "models/robo_link",
				Material = "CelShadingMaterial",
			};

			robot.RegisterMeshSceneNode( jointest );

			robot.RegisterMeshSceneNode( gearNode1 );
			robot.RegisterMeshSceneNode( gearNode2 );
			robot.RegisterMeshSceneNode( gearNode3 );


			ArrayMeshSceneNodePrefab linkNode = new ArrayMeshSceneNodePrefab
			{
				Name = "Links",
				Mesh = "models/robo_link",
				Material = "CelShadingMaterial",

				Path = "Track",
				StartIndex = 0,
				EndIndex = 28
			};

			robot.RegisterArrayMeshSceneNode( linkNode );

			ControllerPrefab gear1Controller = new ControllerPrefab
											   {
												   Name = "Gear1BodyController",
												   Type = ControllerType.BodyController,
												   Body = "Gear1",
												   Transformable = "Gear1"
											   };

			robot.RegisterController( gear1Controller );

			ControllerPrefab gear2Controller = new ControllerPrefab
			{
				Name = "Gear2BodyController",
				Type = ControllerType.BodyController,
				Body = "Gear2",
				Transformable = "Gear2"
			};

			robot.RegisterController( gear2Controller );

			ControllerPrefab gear3Controller = new ControllerPrefab
			{
				Name = "Gear3BodyController",
				Type = ControllerType.BodyController,
				Body = "Gear3",
				Transformable = "Gear3"
			};

			robot.RegisterController( gear3Controller );

			ControllerPrefab lineJointController = new ControllerPrefab
			{
				Name = "Gear1JointController",
				Type = ControllerType.LineJointController,
				Joint = "GearJoint1",
				Transformable = "linkusedasjoint"
			};

			robot.RegisterController( lineJointController );

			return robot;
		}
	}
}
