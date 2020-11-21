using System;
using System.Collections.Generic;
using Ara3D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParticleSimulation.Logic.Controllers;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Physics;
using SFML.Graphics;

namespace ParticleSimulationTests
{
    [TestClass]
    public class CollisionTest
    {
        [TestMethod]
        public void TestCollisionOverlap()
        {
            var physicsController = new PhysicsController();
            var interactions = new Dictionary<int, ParticleInteraction>();
            interactions.Add(1, new ParticleInteraction()
            {
                GraphDots = new List<Vector2>()
                {
                    new Vector2(0, 1),
                    new Vector2(8, 0),
                    new Vector2(16, 1),
                    new Vector2(32, 0)
                }
            });

            var particleType = new ParticleType(1, interactions, Color.Black);
            var particle1 = new Particle(1, particleType, new Vector2());
            var particle2 = new Particle(2, particleType, new Vector2());

            var spaceSnapshot = new SpaceSnapshot()
            {
                AllParticles = new List<Particle>()
                {
                    particle1, particle2
                }
            };


            physicsController.UpdateParticle(new ParticleJobSchedule()
            {
                Particle = particle1,
                SpaceSnapshot = spaceSnapshot
            });

            particle1.Position = particle1.ScheduledPosition;

            physicsController.UpdateParticle(new ParticleJobSchedule()
            {
                Particle = particle2,
                SpaceSnapshot = spaceSnapshot
            });

            Assert.AreNotEqual(particle1.ScheduledPosition, Vector2.Zero);
            Assert.AreNotEqual(particle2.ScheduledPosition, Vector2.Zero);

            Assert.AreNotEqual(particle1.ScheduledPosition, particle2.ScheduledPosition);
        }

        [TestMethod]
        public void TestInterpolation()
        {
            var left = new Vector2(1, 2);
            var right = new Vector2(4, 7);
            var searchPoint = 3.0f;

            var interaction = new ParticleInteraction();
            interaction.GraphDots = new List<Vector2>();

            interaction.GraphDots.Add(left);
            interaction.GraphDots.Add(right);

            Assert.AreEqual(interaction.GetInteraction(searchPoint),
                (left.Y + ((searchPoint - left.X) / (right.X - left.X)) * (right.Y - left.Y))
                ) ;
            
        } 
    }
}
