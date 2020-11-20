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
            var interactions = new Dictionary<int, float>();
            interactions.Add(1, 1);

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
    }
}
