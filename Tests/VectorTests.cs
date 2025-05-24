using Rendering_Engine;
namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VectorAddition()
        {
            Vector3 v = new Vector3(2.0, 2.0, 1.0);
            Vector3 u = new Vector3(1.0, 1.0, 1.0);
            Vector3 w = v + u;

            Assert.Multiple(() =>
            {
                Assert.That(w.X, Is.EqualTo(3.0));
                Assert.That(w.Y, Is.EqualTo(3.0));
                Assert.That(w.Z, Is.EqualTo(2.0));
            });
        }

        [Test]
        public void VectorSubtraction()
        {
            Vector3 v = new Vector3(3.0, 2.0, 2.0);
            Vector3 u = new Vector3(1.0, 1.0, 1.0);
            Vector3 w = v - u;

            Assert.Multiple(() =>
            {
                Assert.That(w.X, Is.EqualTo(2.0));
                Assert.That(w.Y, Is.EqualTo(1.0));
                Assert.That(w.Z, Is.EqualTo(1.0));
            });
        }

        [Test]
        public void VectorMultiplication()
        {
            Vector3 v = new Vector3(2.0, 2.0, 2.0);
            Vector3 u = new Vector3(3.0, 1.0, 3.0);
            Vector3 w = v * u;

            Assert.Multiple(() =>
            {
                Assert.That(w.X, Is.EqualTo(6.0));
                Assert.That(w.Y, Is.EqualTo(2.0));
                Assert.That(w.Z, Is.EqualTo(6.0));
            });
        }

        [Test]
        public void VectorNegation()
        {
            Vector3 v = new Vector3(2.0, 2.0, 2.0);
            Vector3 w = -v;

            Assert.Multiple(() =>
            {
                Assert.That(w.X, Is.EqualTo(-2.0));
                Assert.That(w.Y, Is.EqualTo(-2.0));
                Assert.That(w.Z, Is.EqualTo(-2.0));
            });
        }

        [Test]
        public void ScalarMultiplication()
        {
            Vector3 v = new Vector3(2.0, 2.0, 2.0);
            double t = 2.0;
            Vector3 w = v * t;

            Assert.Multiple(() =>
            {
                Assert.That(w.X, Is.EqualTo(4.0));
                Assert.That(w.Y, Is.EqualTo(4.0));
                Assert.That(w.Z, Is.EqualTo(4.0));
            });
        }

        [Test]
        public void ScalarMultiplication2()
        {
            Vector3 v = new Vector3(2.0, 2.0, 2.0);
            double t = 2.0;
            Vector3 w = t * v;

            Assert.Multiple(() =>
            {
                Assert.That(w.X, Is.EqualTo(4.0));
                Assert.That(w.Y, Is.EqualTo(4.0));
                Assert.That(w.Z, Is.EqualTo(4.0));
            });
        }

        [Test]
        public void ScalarDivision()
        {
            Vector3 v = new Vector3(4.0, 4.0, 4.0);
            double t = 2.0;
            Vector3 w = v / t;

            Assert.Multiple(() =>
            {
                Assert.That(w.X, Is.EqualTo(2.0));
                Assert.That(w.Y, Is.EqualTo(2.0));
                Assert.That(w.Z, Is.EqualTo(2.0));
            });
        }

        [Test]
        public void Length()
        {
            Vector3 v = new Vector3(3.0, 4.0, 0.0);

            Assert.That(v.Length, Is.EqualTo(5));
        }

        [Test]
        public void SquaredLength()
        {
            Vector3 v = new Vector3(3.0, 4.0, 0.0);

            Assert.That(v.SquaredLength, Is.EqualTo(25));
        }
    }
}